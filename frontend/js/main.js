;(function(global, undefined)
{
    'use strict';

    global.Signa = {

        URL: '',

        camera: {},
        reconhecimento: {},
        cenas: {},
        frames: {},

        montarUrlDoServidor: function(caminho) {
            return 'http://localhost:9000/' + caminho;
        }
    };
})(typeof global === 'undefined' ? window : global);

;(function(window, undefined)
{
    'use strict';

    window.View = {
        index: {},
        importar: {}
    };
})(window);

;(function(window, Signa, undefined)
{
    'use strict';
    
    var conexao = $.connection;
    conexao.hub.url = Signa.montarUrlDoServidor('signalr');

    Signa.Hubs = {
        iniciar: function()
        {
            return conexao.hub.start();
        },

        sinais: function()
        {
            return conexao.sinais.server;
        },

        sinaisEstaticos: function()
        {
            return conexao.sinaisEstaticos.server;
        },

        sinaisDinamicos: function()
        {
            return conexao.sinaisDinamicos.server;
        }
    };

})(window, window.Signa);

;(function(window, Signa, undefined)
{
    'use strict';
    
    function LeapRecordingPlayer(leapController)
    {
        leapController
            .use('playback', {
                overlay: false,
                loop: true,
                pauseOnHand: false
            });

        this._player = leapController.plugins.playback.player;
    }

    LeapRecordingPlayer.prototype = {
        _player: undefined,

        loadRecording: function(url)
        {
            this._player.setRecording({
                url: url
            });
        },

        toggle: function()
        {
            this._player.toggle();
        },

        loadFrames: function(loadedFrames, callback)
        {
            var recording = new this._player.Recording({
                loop: true
            });

            recording.readFileData(loadedFrames, callback);

            this._player.setRecording(recording);
        }
    };

    Signa.LeapRecordingPlayer = LeapRecordingPlayer;
})(window, window.Signa);

;(function(window, Signa, undefined)
{
    'use strict';
    
    var CAMERA_NEAR = 0.1;
    var CAMERA_FAR = 1000;
    var CAMERA_FIELD_OF_VIEW = 75;

    function DefaultCameraFactory(aspectRatio)
    {
        this._aspectRatio = aspectRatio;
    }

    DefaultCameraFactory.prototype = {
        _aspectRatio: 0,

        create: function(signaScene)
        {
            var camera = new THREE.PerspectiveCamera(CAMERA_FIELD_OF_VIEW, this._aspectRatio, CAMERA_NEAR, CAMERA_FAR);
            camera.position.set(0, 300, 300);
            camera.rotation.set(-0.5, 0, 0);

            return camera;
        }
    };

    Signa.camera.DefaultCameraFactory = DefaultCameraFactory;
})(window, window.Signa);

;(function(window, Signa, undefined)
{
    'use strict';
    
    function OrbitControlsCameraFactory(cameraFactory)
    {
        this._cameraFactory = cameraFactory;
    }

    OrbitControlsCameraFactory.prototype = {
        _cameraFactory: undefined,
        _orbitControls: undefined,
        _signaScene: undefined,

        create: function(signaScene)
        {
            this._signaScene = signaScene;

            var camera = this._cameraFactory.create();
            this._orbitControls = new THREE.OrbitControls(camera, signaScene.getContainer());
            this._orbitControls.addEventListener('change', this._onControlsChange.bind(this));

            return camera;
        },

        _onControlsChange: function()
        {
            this._signaScene.render();
        }
    };

    Signa.camera.OrbitControlsCameraFactory = OrbitControlsCameraFactory;
})(window, window.Signa);

;(function(window, Signa, undefined)
{
    'use strict';
    
    var idGlobalDeCenas = 0;

    function proximoIdGlobalDeCenas() {
        idGlobalDeCenas++;
        return idGlobalDeCenas;
    }

    function Cena(cameraFactory, container, largura, altura)
    {
        this._id = proximoIdGlobalDeCenas();
        this._scene = new THREE.Scene();
        this._renderer = new THREE.WebGLRenderer();
        this._container = container[0];
        this._camera = cameraFactory.create(this);

        this._renderer.setClearColor(0xF7F7F7);

        this._drawUrs();

        this._renderer.setSize(largura, altura);
        container.append(this._renderer.domElement);
    }

    Cena.prototype = {
        _scene: undefined,
        _camera: undefined,
        _renderer: undefined,
        _id: 0,

        getId: function()
        {
            return this._id;
        },

        render: function()
        {
            this._renderer.render(this._scene, this._camera);
        },

        getThreeScene: function()
        {
            return this._scene;
        },

        getContainer: function()
        {
            return this._container;
        },

        _drawUrs: function()
        {
            var lineMaterial,
                lineGeometry,
                line;

            lineMaterial = new THREE.LineBasicMaterial({color: 0x00FF00});
            lineGeometry = new THREE.Geometry();
            lineGeometry.vertices.push(new THREE.Vector3(0, 0, 0));
            lineGeometry.vertices.push(new THREE.Vector3(0, 100, 0));
            line = new THREE.Line(lineGeometry, lineMaterial);
            this._scene.add(line);

            lineMaterial = new THREE.LineBasicMaterial({color: 0xFF0000});
            lineGeometry = new THREE.Geometry();
            lineGeometry.vertices.push(new THREE.Vector3(0, 0, 0));
            lineGeometry.vertices.push(new THREE.Vector3(100, 0, 0));
            line = new THREE.Line(lineGeometry, lineMaterial);
            this._scene.add(line);

            lineMaterial = new THREE.LineBasicMaterial({color: 0x0000FF});
            lineGeometry = new THREE.Geometry();
            lineGeometry.vertices.push(new THREE.Vector3(0, 0, 0));
            lineGeometry.vertices.push(new THREE.Vector3(0, 0, 100));
            line = new THREE.Line(lineGeometry, lineMaterial);
            this._scene.add(line);
        }
    };

    Signa.cenas.Cena = Cena;
})(window, window.Signa);

;(function(window, Signa, undefined)
{
    'use strict';
    
    function CenaComLeapRiggedHand(leapController, signaScene)
    {
        this._signaScene = signaScene;

        leapController.use('riggedHand', {
            sceneId: signaScene.getId(),
            parent: this.getThreeScene(),
            materialOptions: {
                transparent: true
            },
            renderFn: this.render.bind(this)
        });
    }

    CenaComLeapRiggedHand.prototype = {
        _signaScene: undefined,

        getId: function()
        {
            return this._signaScene.getId();
        },

        render: function()
        {
            this._signaScene.render();
        },

        getThreeScene: function()
        {
            return this._signaScene.getThreeScene();
        },

        getContainer: function()
        {
            return this._signaScene.getContainer();
        }
    };

    Signa.cenas.CenaComLeapRiggedHand = CenaComLeapRiggedHand;
})(window, window.Signa);

;(function(window, Signa, undefined)
{
    'use strict';
    
    Signa.cenas.CenaFactory = {
        criarCenaComLeapRiggedHand: function(largura, altura, container, cameraFactory, leapController) {
            var cena = new Signa.cenas.Cena(cameraFactory, container, largura, altura);
            return new Signa.cenas.CenaComLeapRiggedHand(leapController, cena);
        }
    };
})(window, window.Signa);

;(function(window, Signa, undefined) {
    'use strict';

    var LIMITE_DIFERENCA_ENTRE_NUMEROS = 10;

    function Comparador() {}

    Comparador.prototype = {
        framesSaoIguais: function(frameA, frameB) {
            if (!frameA || !frameB) {
                return false;
            }

            return this._maosSaoIguais(frameA.MaoEsquerda, frameB.MaoEsquerda) &&
                this._maosSaoIguais(frameA.MaoDireita, frameB.MaoDireita);
        },

        _maosSaoIguais: function(maoA, maoB) {
            if (maoA === maoB) {
                return true;
            } else if (!maoA || !maoB) {
                return false;
            }

            var dedosMaoA = maoA.Dedos;
            var dedosMaoB = maoB.Dedos;

            return this._dedosEstaoNaMesmaPosicao(dedosMaoA, dedosMaoB);
        },

        _dedosEstaoNaMesmaPosicao: function(dedosMaoA, dedosMaoB) {
            for (var i = 0; i < dedosMaoA.length; i++) {
                var posicaoDedoMaoA = dedosMaoA[i].PosicaoDaPonta;
                var posicaoDedoMaoB = dedosMaoB[i].PosicaoDaPonta;

                if (dedosMaoA[i].Tipo !== dedosMaoB[i].Tipo) {
                    console.log('DEDOS DE TIPOS DIFERENTES: ' + dedosMaoA[i].Tipo + ', ' + dedosMaoB[i].Tipo);
                }

                if (!arraysSaoIguais(posicaoDedoMaoA, posicaoDedoMaoB)) {
                    return false;
                }
            }
            return true;
        }
    };

    function arraysSaoIguais(arrayA, arrayB) {
        for (var i = 0; i < arrayA.length; i++) {
            if (!numerosSaoIguais(arrayA[i], arrayB[i])) {
                return false;
            }
        }
        return true;
    }

    function numerosSaoIguais(numeroA, numeroB) {
        var diferenca = Math.abs(numeroA - numeroB);
        return diferenca < LIMITE_DIFERENCA_ENTRE_NUMEROS;
    }

    Signa.frames.Comparador = Comparador;
})(window, window.Signa);

;(function(window, Signa, undefined) {
    'use strict';
    
    var ID_EVENTO_FRAME = 'frame';
    var INDICE_DO_FRAME_QUE_DEVE_SER_ARMAZENADO = 2;
    var QUANTIDADE_DE_FRAMES_DO_BUFFER = 4;

    function FrameBuffer() {
        this._eventEmitter = new EventEmitter();
        this._indice = 0;
    }

    FrameBuffer.prototype = {
        _indice: undefined,
        _frame: undefined,
        _eventEmitter: undefined,

        adicionarListenerDeFrame: function(callback) {
            this._eventEmitter.addListener(ID_EVENTO_FRAME, callback);
        },

        onFrame: function(frame) {
            if (this._frameEhValido(frame)) {
                this._indice++;
                if (this._deveArmazenarFrameDoIndice()) {
                    this._frame = frame;
                } else if (this._alcancouMaximoDeFrames()) {
                    this._eventEmitter.trigger(ID_EVENTO_FRAME, [this._frame]);
                    this._indice = 0;
                }
            }
        },

        _frameEhValido: function(frame) {
            return frame.hands.length > 0;
        },

        _deveArmazenarFrameDoIndice: function() {
            return this._indice === INDICE_DO_FRAME_QUE_DEVE_SER_ARMAZENADO;
        },

        _alcancouMaximoDeFrames: function() {
            return this._indice === QUANTIDADE_DE_FRAMES_DO_BUFFER;
        }
    };

    FrameBuffer.doLeapController = function(leapController) {
        var frameBuffer = new FrameBuffer();
        leapController.on('frame', frameBuffer.onFrame.bind(frameBuffer));
        return frameBuffer;
    };


    Signa.frames.FrameBuffer = FrameBuffer;
})(window, window.Signa);

;(function(global, Signa, undefined) {
    'use strict';

    function InformacoesDoFrame(){}
    InformacoesDoFrame.prototype = {
        extrairParaAmostra: function(frame) {
            return {
                MaoEsquerda: this._extrairDadosDaMaoEsquerda(frame.hands),
                MaoDireita: this._extrairDadosDaMaoDireita(frame.hands)
            };
        },

        _extrairDadosDaMaoEsquerda: function(maos) {
            if (this._ehMaoEsquerda(maos[0]))
                return this._extrairDadosDaMao(maos[0]);

            if (this._ehMaoEsquerda(maos[1]))
                return this._extrairDadosDaMao(maos[1]);

            return null;
        },

        _ehMaoEsquerda: function(hand) {
            return hand && hand.type.toUpperCase() === 'LEFT';
        },

        _extrairDadosDaMaoDireita: function(maos) {
            if (this._ehMaoDireita(maos[0]))
                return this._extrairDadosDaMao(maos[0]);

            if (this._ehMaoDireita(maos[1]))
                return this._extrairDadosDaMao(maos[1]);

            return null;
        },

        _ehMaoDireita: function(hand) {
            return hand && hand.type.toUpperCase() === 'RIGHT';
        },

        _extrairDadosDaMao: function(leapHand) {
            return {
                VetorNormalDaPalma: leapHand.palmNormal,
                PosicaoDaPalma: leapHand.stabilizedPalmPosition,
                VelocidadeDaPalma: leapHand.palmVelocity,
                Direcao: leapHand.direction,
                Dedos: this._extrairDadosDosDedos(leapHand.fingers),
                RaioDaEsfera: leapHand.sphereRadius,
                Pitch: leapHand.pitch(),
                Roll: leapHand.roll(),
                Yaw: leapHand.yaw()
            };
        },

        _extrairDadosDosDedos: function(leapFingers) {
            var dedos = new Array(leapFingers.length);

            for (var i = 0; i < dedos.length; i++) {
                dedos[i] = {
                    Tipo: leapFingers[i].type,
                    Direcao: leapFingers[i].direction,
                    PosicaoDaPonta: leapFingers[i].stabilizedTipPosition,
                    VelocidadeDaPonta: leapFingers[i].tipVelocity,
                    Apontando: leapFingers[i].extended
                };
            }

            return dedos;
        }
    };

    Signa.frames.InformacoesDoFrame = InformacoesDoFrame;
})(window, window.Signa);

;(function(window, View, Signa, undefined) {
    'use strict';

    function Importar(){}

    Importar.prototype = {
        _leapRecordingPlayer: undefined,
        _framesCarregados: undefined,
        _frameSignDataProcessor: undefined,
        _framesCarregadosEmFormatoJson: undefined,

        iniciar: function() {
            var leapController = new Leap.Controller();

            Signa.Hubs.iniciar();
            this._iniciarCena(leapController);

            this._leapRecordingPlayer = new Signa.LeapRecordingPlayer(leapController);
            this._frameSignDataProcessor = new Signa.frames.InformacoesDoFrame();

            $('#message').hide();
            $('#sign-file').change(this._onArquivoDoSinalChange.bind(this));
            $('#save').click(this._onSalvarClick.bind(this));
        },

        _iniciarCena: function(leapController) {
            var largura = $("#handmodel-user").width(),
                altura = $("#handmodel-user").height(),
                container = $("#handmodel-user"),
                cameraFactory = new Signa.camera.DefaultCameraFactory(largura / altura),
                cenaDaMaoDoUsuario;

            cameraFactory = new Signa.camera.OrbitControlsCameraFactory(cameraFactory);

            cenaDaMaoDoUsuario = new Signa.cenas.Cena(cameraFactory, container, largura, altura);
            cenaDaMaoDoUsuario = new Signa.cenas.CenaComLeapRiggedHand(leapController, cenaDaMaoDoUsuario);
            
            cenaDaMaoDoUsuario.render();
        },

        _onArquivoDoSinalChange: function(event) {
            var arquivo = event.target.files[0];
            this._lerArquivoDoSinal(arquivo);  
        },

        _lerArquivoDoSinal: function(arquivo) {
            var leitorDeArquivo = new FileReader();

            leitorDeArquivo.onload = function(event) {
                this._framesCarregadosEmFormatoJson = event.target.result;
                var framesCarregados = JSON.parse(this._framesCarregadosEmFormatoJson);
                
                this._leapRecordingPlayer.loadFrames(framesCarregados, function(frames) {
                    this._framesCarregados = frames;
                }.bind(this));
            }.bind(this);

            leitorDeArquivo.readAsText(arquivo);
        },

        _onSalvarClick: function() {
            this._salvarAmostraDoSinal();
        },

        _salvarAmostraDoSinal: function() {
            var descricaoDoSinal = $('#description').val(),
                amostra = this._gerarAmostra();

            this._enviarInformacoesParaOServidor(descricaoDoSinal, amostra);
        },

        _gerarAmostra: function() {
            debugger;
            if (this._framesCarregados.length === 1) {
                return this._gerarAmostraEstatica();
            }

            return this._gerarAmostraDinamica();
        },

        _gerarAmostraEstatica: function() {
            var frame = new Leap.Frame(this._framesCarregados[0]),
                frameDaAmostra = this._frameSignDataProcessor.extrairParaAmostra(frame);

            return [frameDaAmostra];
        },

        _gerarAmostraDinamica: function() {
            var framesCarregados = this._framesCarregados,
                amostra = [],
                frameBuffer = new Signa.frames.FrameBuffer();

            frameBuffer.adicionarListenerDeFrame(function(frame) {
                var leapFrame = new Leap.Frame(frame),
                    frameDaAmostra = this._frameSignDataProcessor.extrairParaAmostra(leapFrame);

                amostra.push(frameDaAmostra);
            }.bind(this));

            framesCarregados.forEach(function(frame) {
                frameBuffer.onFrame(frame);
            });

            return amostra;
        },

        _enviarInformacoesParaOServidor: function(descricaoDoSinal, amostra) {
            var url = this._montarUrlParaSalvarOSinal(amostra);
            $('#message').text('Salvando informações do sinal...').show();
            debugger;
            $.post(url, {
                descricao: descricaoDoSinal,
                conteudoDoArquivoDeExemplo: this._framesCarregadosEmFormatoJson,
                amostra: amostra
            }).done(function() {
                $('#message').text('Informações salvas com sucesso!');
                setTimeout(function() {
                    $('#message').hide();
                }, 2000);
            });
        },

        _montarUrlParaSalvarOSinal: function(amostra) {
            if (this._ehAmostraDeSinalEstatico(amostra)) {
                return Signa.montarUrlDoServidor('sinais/SalvarAmostraDeSinalEstatico');
            } else {
                return Signa.montarUrlDoServidor('sinais/SalvarAmostraDeSinalDinamico');
            }
        },

        _ehAmostraDeSinalEstatico: function(amostra) {
            return amostra.length === 1;
        }
    };


    View.importar.Importar = Importar;
})(window, window.View, window.Signa);

;(function(window, View, Signa, undefined)
{
    'use strict';

    function ContainerComDescricaoDoSinal(textContainer)
    {
        this._container = textContainer;
    }

    ContainerComDescricaoDoSinal.prototype = {
        _container: undefined,

        onNewSign: function(informacoesDoSinal)
        {
            this._container
                .text(informacoesDoSinal.Descricao);
        },

        onRecognize: function()
        {
        }
    };

    View.index.ContainerComDescricaoDoSinal = ContainerComDescricaoDoSinal;
})(window, window.View, window.Signa);

;(function(window, View, Signa, undefined)
{
    'use strict';
    
    function ContainerComExemploDoSinal(cameraFactory, container, leapController, largura, altura)
    {
        var cameraComControlesFactory = new Signa.camera.OrbitControlsCameraFactory(cameraFactory);
        Signa.cenas.CenaFactory.criarCenaComLeapRiggedHand(largura, altura, container, cameraComControlesFactory, leapController);

        this._leapRecordingPlayer = new Signa.LeapRecordingPlayer(leapController);

        $('#play-pause').click(this._onPlayPause.bind(this));
    }

    ContainerComExemploDoSinal.prototype = {
        _leapRecordingPlayer: undefined,

        onNewSign: function(informacoesDoSinal)
        {
            this._leapRecordingPlayer.loadRecording('http://localhost:9000/' + informacoesDoSinal.CaminhoParaArquivoDeExemplo);
        },

        onRecognize: function()
        {

        },

        _onPlayPause: function()
        {
            this._leapRecordingPlayer.toggle();
        }
    };

    View.index.ContainerComExemploDoSinal = ContainerComExemploDoSinal;
})(window, window.View, window.Signa);

;(function(window, View, Signa, undefined)
{
    'use strict';
    
    function ContainerComMaosDoUsuario(cameraFactory, container, leapController, largura, altura)
    {
        this._container = container;

        Signa.cenas.CenaFactory.criarCenaComLeapRiggedHand(largura, altura, container, cameraFactory, leapController);
    }

    ContainerComMaosDoUsuario.prototype = {
        _container: undefined,

        onNewSign: function(signInfo)
        {
            this._container
                .addClass('signa-handmodel-user-error')
                .removeClass('signa-handmodel-user-success');
        },

        onRecognize: function()
        {
            this._container
                .removeClass('signa-handmodel-user-error')
                .addClass('signa-handmodel-user-success');
        }
    };

    View.index.ContainerComMaosDoUsuario = ContainerComMaosDoUsuario;
})(window, window.View, window.Signa);

;(function(window, View, Signa, undefined)
{
    'use strict';

    function Index(){}

    Index.RECOGNIZE_EVENT_ID = 'recognize';
    Index.NEW_SIGN_EVENT_ID = 'new-sign';

    Index.prototype = {
        _descricaoDoSinal: undefined,
        _maosDoUsuario: undefined,
        _exemploDoSinal: undefined,
        _reconhecedorDeSinais: undefined,
        _informacoesDoSinal: undefined,
        _messageBox: undefined,

        init: function()
        {
            var width = $("#handmodel-user").width(),
                height = $("#handmodel-user").height(),
                cameraFactory = new Signa.camera.DefaultCameraFactory(width / height);

            this._messageBox = $('#recognized-sign-message');

            this._descricaoDoSinal = new View.index.ContainerComDescricaoDoSinal($('#sign-description'));
            this._iniciarExemploDoSinal(cameraFactory, width, height);
            this._iniciarMaosDoUsuario(cameraFactory, width, height);

            this._carregarProximoSinal();
        },

        _iniciarExemploDoSinal: function(cameraFactory, width, height)
        {
            var signExampleLeapController = new Leap.Controller(),
                container = $("#handmodel-example");

            this._exemploDoSinal = new View.index.ContainerComExemploDoSinal(cameraFactory, container, signExampleLeapController, width, height);

            signExampleLeapController.on('playback.recordingSet', this._onNewSignLoad.bind(this));
        },

        _iniciarMaosDoUsuario: function(cameraFactory, width, height)
        {
            var leapController = new Leap.Controller(),
                container = $("#handmodel-user"),
                frameBuffer = Signa.frames.FrameBuffer.doLeapController(leapController);

            this._maosDoUsuario = new View.index.ContainerComMaosDoUsuario(cameraFactory, container, leapController, width, height);
            
            leapController.connect();

            this._reconhecedorDeSinais = new Signa.reconhecimento.ReconhecedorDeSinais(frameBuffer);
            this._reconhecedorDeSinais.adicionarListenerDeReconhecimento(this._onRecognize.bind(this));
        },

        _onNewSign: function(informacoesDoSinal)
        {
            this._informacoesDoSinal = informacoesDoSinal;
            this._exemploDoSinal.onNewSign(informacoesDoSinal);
        },

        _onNewSignLoad: function()
        {
            this._descricaoDoSinal.onNewSign(this._informacoesDoSinal);
            this._maosDoUsuario.onNewSign();
            this._reconhecedorDeSinais.setTipoDoSinal(this._informacoesDoSinal.Tipo);
            this._reconhecedorDeSinais.setIdDoSinalParaReconhecer(this._informacoesDoSinal.Id);
            this._hideRecognizeMessage();
        },

        _hideRecognizeMessage: function()
        {
            this._messageBox.hide();
        },

        _onRecognize: function()
        {
            this._showRecognizeMessage();
            this._descricaoDoSinal.onRecognize();
            this._exemploDoSinal.onRecognize();
            this._maosDoUsuario.onRecognize();
            this._reconhecedorDeSinais.setIdDoSinalParaReconhecer(-1);
            window.setTimeout(this._carregarProximoSinal.bind(this), 1000);
        },

        _showRecognizeMessage: function()
        {
            this._messageBox.show();
        },

        _carregarProximoSinal: function()
        {
            Signa.Hubs
                .iniciar()
                .done(function() {
                    var id = this._informacoesDoSinal ? this._informacoesDoSinal.Id : -1;
                    Signa.Hubs
                        .sinais()
                        .proximoSinal(id)
                        .done(this._onNewSign.bind(this));
                }.bind(this));
        }
    };


    View.index.Index = Index;
})(window, window.View, window.Signa);

;(function(window, Signa, undefined) {
    'use strict';

    var EVENTO_RECONHECEU_SINAL = 'reconheceu';

    function ReconhecedorDeSinais(frameBuffer) {
        var me = this;
        
        me._estado = new Signa.reconhecimento.ReconhecedorDeSinaisOffline();
        me._eventEmitter = new EventEmitter();

        frameBuffer.adicionarListenerDeFrame(this._onFrame.bind(this));

        Signa.Hubs.iniciar()
            .done(function() {
                var tipoDoSinal = me._estado.getTipoDoSinal(),
                    idDoSinal = me._estado.getIdDoSinal();

                me._estado = new Signa.reconhecimento.ReconhecedorDeSinaisOnline();

                me.setTipoDoSinal(tipoDoSinal);
                me.setIdDoSinalParaReconhecer(idDoSinal);
            });
    }

    ReconhecedorDeSinais.prototype = {
        _estado: undefined,
        _eventEmitter: undefined,

        adicionarListenerDeReconhecimento: function(listener) {
            this._eventEmitter.addListener(EVENTO_RECONHECEU_SINAL, listener);
        },

        setIdDoSinalParaReconhecer: function(idDoSinalParaReconhecer) {
            this._estado.setIdDoSinalParaReconhecer(idDoSinalParaReconhecer);
        },

        setTipoDoSinal: function(tipoDoSinal) {
            this._estado.setTipoDoSinal(tipoDoSinal);
        },
        
        _onFrame: function(frame) {
            this._estado
                .reconhecer(frame)
                .then(function(sinalFoiReconhecido) {
                    if (sinalFoiReconhecido) {
                        this._eventEmitter.trigger(EVENTO_RECONHECEU_SINAL);
                    }
                }.bind(this));
        }
    };

    Signa.reconhecimento.ReconhecedorDeSinais = ReconhecedorDeSinais;
})(window, window.Signa);

;(function(window, Signa, undefined) {
    'use strict';

    function ReconhecedorDeSinaisOffline() {}

    ReconhecedorDeSinaisOffline.prototype = {
        _idDoSinalParaReconhecer: -1,
        _tipoDoSinal: -1,

        reconhecer: function() {
            return Promise.resolve(false);
        },

        setIdDoSinalParaReconhecer: function(idDoSinalParaReconhecer) {
            this._idDoSinalParaReconhecer = idDoSinalParaReconhecer;
        },

        setTipoDoSinal: function(tipoDoSinal) {
            this._tipoDoSinal = tipoDoSinal;
        },

        getIdDoSinal: function() {
            return this._idDoSinalParaReconhecer;
        },

        getTipoDoSinal: function() {
            return this._tipoDoSinal;
        }
    };

    Signa.reconhecimento.ReconhecedorDeSinaisOffline = ReconhecedorDeSinaisOffline;
})(window, window.Signa);

;(function(window, Signa, undefined) {
    'use strict';

    function ReconhecedorDeSinaisOnline() {
        this._informacoesDoFrame = new Signa.frames.InformacoesDoFrame();
    }

    ReconhecedorDeSinaisOnline.prototype = {
        _informacoesDoFrame: undefined,
        _idDoSinalParaReconhecer: -1,
        _algoritmo: undefined,
        _tipoDoSinal: -1,

        getIdDoSinal: function() {
            return this._idDoSinalParaReconhecer;
        },

        getTipoDoSinal: function() {
            return this._tipoDoSinal;
        },

        reconhecer: function(frame) {
            if (this._idDoSinalParaReconhecer === -1)
                return Promise.resolve(false);

            var dados = this._informacoesDoFrame.extrairParaAmostra(frame);
            return this._algoritmo
                .reconhecer(dados)
                .then(function(sinalFoiReconhecido) {
                    if (sinalFoiReconhecido) {
                        this._idDoSinalParaReconhecer = -1;
                    }
                    return sinalFoiReconhecido;
                }.bind(this));
        },

        setIdDoSinalParaReconhecer: function(id) {
            this._idDoSinalParaReconhecer = id;
            this._algoritmo.setSinalId(id);
        },

        setTipoDoSinal: function(tipoDoSinal) {
            this._tipoDoSinal = tipoDoSinal;
            if (this._ehSinalEstatico(tipoDoSinal)) {
                this._algoritmo = new Signa.reconhecimento.SinalEstatico();
            } else {
                this._algoritmo = new Signa.reconhecimento.SinalDinamico();
            }
        },

        _ehSinalEstatico: function(tipoDoSinal) {
            return tipoDoSinal === 0;
        }
    };

    Signa.reconhecimento.ReconhecedorDeSinaisOnline = ReconhecedorDeSinaisOnline;
})(window, window.Signa);

;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamico() {
        this.RECONHECENDO = new Signa.reconhecimento.SinalDinamicoReconhecendo(this);
        
        this.NAO_RECONHECEU_FRAME = 
            new Signa.reconhecimento.SinalDinamicoNaoReconheceuFrame(this);
        
        this.RECONHECEU_PRIMEIRO_FRAME = 
            new Signa.reconhecimento.SinalDinamicoReconheceuPrimeiroFrame(this);
        
        this.RECONHECEU_ULTIMO_FRAME = 
            new Signa.reconhecimento.SinalDinamicoReconheceuUltimoFrame(this, this.RECONHECENDO);

        this._estado = this.NAO_RECONHECEU_FRAME;
        this._buffer = this.RECONHECENDO;
        this._comparador = new Signa.frames.Comparador();
    }

    SinalDinamico.prototype = {
        _estado: undefined,
        _buffer: undefined,
        _comparador: undefined,
        _ultimoFrame: undefined,
        _sinalId: -1,

        setSinalId: function(sinalId) {
            console.log('SINAL ' + sinalId);
            this._sinalId = sinalId;
        },

        getSinalId: function() {
            return this._sinalId;
        },

        reconhecer: function(frame) {
            if (this._comparador.framesSaoIguais(frame, this._ultimoFrame)) {
                return Promise.resolve(false);
            }

            var estado = this._estado,
                amostra = [frame];

            this._ultimoFrame = frame;
            this.reconhecendo(amostra);

            return estado.reconhecer(amostra);
        },

        reconhecendo: function(amostra) {
            this._estado = this.RECONHECENDO;
            this._salvarAmostraNoBuffer(amostra);
        },

        _salvarAmostraNoBuffer: function(amostra) {
            this._estado.reconhecer(amostra);
        },

        naoReconheceuFrame: function() {
            this._buffer.limpar();
            this._estado = this.NAO_RECONHECEU_FRAME;
        },

        reconheceuPrimeiroFrame: function() {
            this._estado = this.RECONHECEU_PRIMEIRO_FRAME;
        },

        reconheceuUltimoFrame: function() {
            this._estado = this.RECONHECEU_ULTIMO_FRAME;
        }
    };

    Signa.reconhecimento.SinalDinamico = SinalDinamico;
})(window, window.Signa);

;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamicoNaoReconheceuFrame(algoritmoDeSinalDinamico) {
        this._algoritmoDeSinalDinamico = algoritmoDeSinalDinamico;
    }

    SinalDinamicoNaoReconheceuFrame.prototype = {
        _algoritmoDeSinalDinamico: undefined,
        
        reconhecer: function(amostra) {
            var algoritmoDeSinalDinamico = this._algoritmoDeSinalDinamico;

            return Signa.Hubs.sinaisDinamicos()
                .reconhecerPrimeiroFrame(amostra)
                .then(function(id) {
                    if (algoritmoDeSinalDinamico.getSinalId() === id) {
                        algoritmoDeSinalDinamico.reconheceuPrimeiroFrame();
                        return false;
                    }
                    algoritmoDeSinalDinamico.naoReconheceuFrame();
                    return false;
                });
        }
    };

    Signa.reconhecimento.SinalDinamicoNaoReconheceuFrame = SinalDinamicoNaoReconheceuFrame;
})(window, window.Signa);

;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamicoReconhecendo() {
        this._frames = [];
        this._deveArmazenarOsFrames = false;
    }

    SinalDinamicoReconhecendo.prototype = {
        _frames: undefined,

        getFrames: function() {
            return this._frames;
        },  

        reconhecer: function(amostra) {
            console.log('guardando sinal');
            this._frames.push(amostra[0]);

            return Promise.resolve(false);
        },

        limpar: function() {
            console.log('limpando sinais');
            this._frames = [];
        }
    };

    Signa.reconhecimento.SinalDinamicoReconhecendo = SinalDinamicoReconhecendo;
})(window, window.Signa);

;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamicoReconheceuPrimeiroFrame(algoritmoDeSinalDinamico) {
        this._algoritmoDeSinalDinamico = algoritmoDeSinalDinamico;
    }

    SinalDinamicoReconheceuPrimeiroFrame.prototype = {
        _algoritmoDeSinalDinamico: undefined,

        reconhecer: function(amostra) {
            var algoritmoDeSinalDinamico = this._algoritmoDeSinalDinamico;

            return Signa.Hubs.sinaisDinamicos()
                .reconhecerUltimoFrame(amostra)
                .then(function(id) {
                    if (algoritmoDeSinalDinamico.getSinalId() === id) {
                        algoritmoDeSinalDinamico.reconheceuUltimoFrame();
                        return false;
                    }
                    algoritmoDeSinalDinamico.reconheceuPrimeiroFrame();
                    return false;
                });
        }
    };

    Signa.reconhecimento.SinalDinamicoReconheceuPrimeiroFrame = SinalDinamicoReconheceuPrimeiroFrame;
})(window, window.Signa);

;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamicoReconheceuUltimoFrame(algoritmoDeSinalDinamico, buffer) {
        this._algoritmoDeSinalDinamico = algoritmoDeSinalDinamico;
        this._buffer = buffer;
    }

    SinalDinamicoReconheceuUltimoFrame.prototype = {
        _algoritmoDeSinalDinamico: undefined,
        
        reconhecer: function() {
            var algoritmoDeSinalDinamico = this._algoritmoDeSinalDinamico;
            
            return Signa.Hubs.sinaisDinamicos()
                .reconhecer(this._buffer.getFrames())
                .then(function(id) {
                    algoritmoDeSinalDinamico.naoReconheceuFrame();
                    
                    return algoritmoDeSinalDinamico.getSinalId() === id;
                });
        }
    };

    Signa.reconhecimento.SinalDinamicoReconheceuUltimoFrame = SinalDinamicoReconheceuUltimoFrame;
})(window, window.Signa);

;(function(window, Signa, undefined) {
    'use strict';

    function SinalEstatico() {
        this._hub = Signa.Hubs.sinaisEstaticos();
    }

    SinalEstatico.prototype = {
        _hub: undefined,
        _sinalId: -1,

        setSinalId: function(sinalId) {
            this._sinalId = sinalId;
        },

        reconhecer: function(frame) {
            return this._hub
                .reconhecer([frame])
                .then(function(sinalReconhecidoId) {
                    return sinalReconhecidoId === this._sinalId;
                }.bind(this));
        }
    };

    Signa.reconhecimento.SinalEstatico = SinalEstatico;
})(window, window.Signa);
