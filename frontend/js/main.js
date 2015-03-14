;(function(global, undefined)
{
    'use strict';

    global.Signa = {

        camera: {},
        recognizer: {},
        scene: {},

        signalrHub: function()
        {
            return getConnection().signSequence.server;
        },

        initHubs: function()
        {
            return getConnection().hub.start();
        }
    };
})(typeof global === 'undefined' ? window : global);

;(function(window, undefined)
{
    'use strict';

    window.View = {
        index: {},
        load: {}
    };
})(window);

;(function(window, Signa, undefined)
{
    'use strict';
    
    var conexao = $.connection;
    conexao.hub.url = 'http://localhost:9000/signalr';

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
    
    function RiggedHandScene(leapController, signaScene)
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

    RiggedHandScene.prototype = {
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

    Signa.scene.RiggedHandScene = RiggedHandScene;
})(window, window.Signa);

;(function(window, Signa, undefined)
{
    'use strict';
    
    var globalSceneId = 0;
    function Scene(cameraFactory, container, width, height)
    {
        this._id = globalSceneId;
        globalSceneId++;
        this._scene = new THREE.Scene();
        this._renderer = new THREE.WebGLRenderer();
        this._container = container[0];
        this._camera = cameraFactory.create(this);

        this._renderer.setClearColor(0xF7F7F7);

        this._drawUrs();

        this._renderer.setSize(width, height);
        container.append(this._renderer.domElement);
    }

    Scene.prototype = {
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

    Signa.scene.Scene = Scene;
})(window, window.Signa);

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

            this._descricaoDoSinal = new View.index.SignDescription($('#sign-description'));
            this._iniciarExemploDoSinal(cameraFactory, width, height);
            this._iniciarMaosDoUsuario(cameraFactory, width, height);

            this._carregarProximoSinal();
        },

        _iniciarExemploDoSinal: function(cameraFactory, width, height)
        {
            var signExampleLeapController = new Leap.Controller(),
                container = $("#handmodel-example");

            this._exemploDoSinal = new View.index.SignExample(cameraFactory, container, signExampleLeapController, width, height);

            signExampleLeapController.on('playback.recordingSet', this._onNewSignLoad.bind(this));
        },

        _iniciarMaosDoUsuario: function(cameraFactory, width, height)
        {
            var userHandsLeapController = new Leap.Controller(),
                container = $("#handmodel-user");

            this._maosDoUsuario = new View.index.UserHands(cameraFactory, container, userHandsLeapController, width, height);
            
            userHandsLeapController.connect();
            
            this._reconhecedorDeSinais = new Signa.recognizer.SignRecognizer(userHandsLeapController);
            this._reconhecedorDeSinais.addRecognizeEventListener(this._onRecognize.bind(this));
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
            this._reconhecedorDeSinais.setSignToRecognizeId(this._informacoesDoSinal.Id);
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
            this._reconhecedorDeSinais.setSignToRecognizeId(-1);
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

;(function(window, View, Signa, undefined)
{
    'use strict';

    function SignDescription(textContainer)
    {
        this._textContainer = textContainer;
    }

    SignDescription.prototype = {
        _textContainer: undefined,

        onNewSign: function(signInfo)
        {
            this._textContainer
                .text(signInfo.Description);
        },

        onRecognize: function()
        {
        }
    };

    View.index.SignDescription = SignDescription;
})(window, window.View, window.Signa);

;(function(window, View, Signa, undefined)
{
    'use strict';
    
    function SignExample(cameraFactory, container, leapController, width, height)
    {
        var orbitConstrolsCameraFactory = new Signa.camera.OrbitControlsCameraFactory(cameraFactory),
            exampleHandmodelScene = new Signa.scene.Scene(orbitConstrolsCameraFactory, container, width, height);

        exampleHandmodelScene = new Signa.scene.RiggedHandScene(leapController, exampleHandmodelScene);

        this._leapRecordingPlayer = new Signa.LeapRecordingPlayer(leapController);

        $('#play-pause').click(this._onPlayPause.bind(this));
    }

    SignExample.prototype = {
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

    View.index.SignExample = SignExample;
})(window, window.View, window.Signa);

;(function(window, View, Signa, undefined)
{
    'use strict';
    
    function UserHands(cameraFactory, container, leapController, width, height)
    {
        var userHandmodelScene = new Signa.scene.Scene(cameraFactory, container, width, height);

        this._userRiggedHand = new Signa.scene.RiggedHandScene(leapController, userHandmodelScene);

        this._container = container;
    }

    UserHands.prototype = {
        _userRiggedHand: undefined,
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

    View.index.UserHands = UserHands;
})(window, window.View, window.Signa);

;(function(window, View, Signa, undefined)
{
    'use strict';

    function Load(){}

    Load.prototype = {
        _leapRecordingPlayer: undefined,
        _framesCarregados: undefined,
        _frameSignDataProcessor: undefined,
        _framesCarregadosEmFormatoJson: undefined,

        init: function()
        {
            var leapController = new Leap.Controller();

            Signa.Hubs.iniciar();
            this._iniciarCena(leapController);

            this._leapRecordingPlayer = new Signa.LeapRecordingPlayer(leapController);
            this._frameSignDataProcessor = new Signa.recognizer.FrameSignDataProcessor();

            $('#sign-file').change(this._onArquivoDoSinalChange.bind(this));
            $('#save').click(this._onSalvarClick.bind(this));
        },

        _iniciarCena: function(leapController)
        {
            var largura = $("#handmodel-user").width(),
                altura = $("#handmodel-user").height(),
                container = $("#handmodel-user"),
                cameraFactory = new Signa.camera.DefaultCameraFactory(largura / altura),
                cenaDaMaoDoUsuario;

            cameraFactory = new Signa.camera.OrbitControlsCameraFactory(cameraFactory);

            cenaDaMaoDoUsuario = new Signa.scene.Scene(cameraFactory, container, largura, altura);
            cenaDaMaoDoUsuario = new Signa.scene.RiggedHandScene(leapController, cenaDaMaoDoUsuario);
            
            cenaDaMaoDoUsuario.render();
        },

        _onArquivoDoSinalChange: function(event)
        {
            var arquivo = event.target.files[0];
            this._lerArquivoDoSinal(arquivo);  
        },

        _lerArquivoDoSinal: function(arquivo)
        {
            var leitorDeArquivo = new FileReader();

            leitorDeArquivo.onload = function(event)
            {
                this._framesCarregadosEmFormatoJson = event.target.result;
                var framesCarregados = JSON.parse(this._framesCarregadosEmFormatoJson);
                
                this._leapRecordingPlayer.loadFrames(framesCarregados, function(frames)
                {
                    this._framesCarregados = frames;
                }.bind(this));
            }.bind(this);

            leitorDeArquivo.readAsText(arquivo);
        },

        _onSalvarClick: function()
        {
            this._saveSignSample();
        },

        _saveSignSample: function()
        {
            var descricaoDoSinal = $('#description').val(),
                amostra = this._gerarAmostra(),
                hub;

            if (amostra.length === 1) {
                hub = Signa.Hubs.sinaisEstaticos();
            } else {
                hub = Signa.Hubs.sinaisDinamicos();
            }

            hub.salvarAmostraDoSinal(descricaoDoSinal, '', amostra);
        },

        _gerarAmostra: function()
        {
            var framesCarregados = this._framesCarregados,
                amostra = new Array(framesCarregados.length);

            for (var i = 0; i < amostra.length; i++) {
                var frame = new Leap.Frame(this._framesCarregados[0]);
                amostra[i] = this._frameSignDataProcessor.extrairFrameDaAmostra(frame);
            }
            
            return amostra;
        }
    };


    View.load.Load = Load;
})(window, window.View, window.Signa);

;(function(global, Signa, undefined)
{
    'use strict';

    function FrameSignDataProcessor(){}
    FrameSignDataProcessor.prototype = {
        extrairFrameDaAmostra: function(frame)
        {
            return {
                MaoEsquerda: this._extrairDadosDaMaoEsquerda(frame.hands),
                MaoDireita: this._extrairDadosDaMaoDireita(frame.hands)
            };
        },

        _extrairDadosDaMaoEsquerda: function(maos)
        {
            if (this._ehMaoEsquerda(maos[0]))
                return this._extrairDadosDaMao(maos[0]);

            if (this._ehMaoEsquerda(maos[1]))
                return this._extrairDadosDaMao(maos[1]);

            return null;
        },

        _ehMaoEsquerda: function(hand)
        {
            return hand && hand.type.toUpperCase() === 'LEFT';
        },

        _extrairDadosDaMaoDireita: function(maos)
        {
            if (this._ehMaoDireita(maos[0]))
                return this._extrairDadosDaMao(maos[0]);

            if (this._ehMaoDireita(maos[1]))
                return this._extrairDadosDaMao(maos[1]);

            return null;
        },

        _ehMaoDireita: function(hand)
        {
            return hand && hand.type.toUpperCase() === 'RIGHT';
        },

        _extrairDadosDaMao: function(leapHand)
        {
            return {
                VetorNormalDaPalma: leapHand.palmNormal,
                Direcao: leapHand.direction,
                Dedos: this._extrairDadosDosDedos(leapHand.fingers)
            };
        },

        _extrairDadosDosDedos: function(leapFingers)
        {
            var dedos = new Array(leapFingers.length);

            for (var i = 0; i < dedos.length; i++)
            {
                dedos[i] = {
                    tipo: leapFingers[i].type,
                    direcao: leapFingers[i].direction
                };
            }

            return dedos;
        }
    };

    Signa.recognizer.FrameSignDataProcessor = FrameSignDataProcessor;
})(global = typeof global === 'undefined' ? window : global, global.Signa);

;(function(window, Signa, undefined)
{
    'use strict';

    function OfflineSignRecognizer(eventEmitter)
    {
        this._eventEmitter = eventEmitter;
    }

    OfflineSignRecognizer.prototype = {
        _eventEmitter: undefined,

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(){},

        setSignToRecognizeId: function(){}
    };

    Signa.recognizer.OfflineSignRecognizer = OfflineSignRecognizer;
})(window, window.Signa);

;(function(window, Signa, undefined)
{
    'use strict';

    function OnlineSignRecognizer(signalRecognizer, eventEmitter)
    {
        this._eventEmitter = eventEmitter;
        this._signalRecognizer = signalRecognizer;
    }

    OnlineSignRecognizer.prototype = {
        _eventEmitter: undefined,
        _signalRecognizer: undefined,

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(){},

        setSignToRecognizeId: function(){}
    };

    Signa.recognizer.OnlineSignRecognizer = OnlineSignRecognizer;
})(window, window.Signa);

;(function(window, Signa, undefined)
{
    'use strict';

    function SignRecognizer(leapController)
    {
        var me = this;
        leapController.on('frame', this._onLeapFrame.bind(this));
        me._eventEmitter = new EventEmitter();
        me.OFFLINE = new Signa.recognizer.OfflineSignRecognizer(this._eventEmitter);
        me.ONLINE = new Signa.recognizer.OnlineSignRecognizer(this, this._eventEmitter);
        me.TRAINED = new Signa.recognizer.TrainedSignRecognizer(this._eventEmitter);

        me._estado = me.OFFLINE;

        Signa.Hubs.iniciar().done(function()
        {
            me._estado = me.TRAINED;
        });
    }

    SignRecognizer.prototype = {
        RECOGNIZE_EVENT_ID: 'recognize',
        OFFLINE: undefined,
        ONLINE: undefined,
        TRAINED: undefined,

        _eventEmitter: undefined,
        _estado: undefined,
        _frame: undefined,
        _idDoSinalParaReconhecer: -1,

        setState: function(state)
        {
            this._estado = state;
            state.setSignalToRecognizeId(this._idDoSinalParaReconhecer);
        },

        setSignToRecognizeId: function(signalToRecognizeId)
        {
            this._estado.setSignToRecognizeId(signalToRecognizeId);
            this._idDoSinalParaReconhecer = signalToRecognizeId;
        },

        _reconhecer: function(frame)
        {
            this._estado.recognize(frame);
        },

        _onLeapFrame: function(frame)
        {
            // pegar os dados necessários do frame
            this._frame = frame;
            this._reconhecer(frame);
        },

        addRecognizeEventListener: function(listener)
        {
            this._estado.addRecognizeEventListener(listener);
        }
    };

    Signa.recognizer.SignRecognizer = SignRecognizer;
})(window, window.Signa);

;(function(window, Signa, undefined)
{
    'use strict';

    function TrainedSignRecognizer(eventEmitter)
    {
        this._eventEmitter = eventEmitter;
        this._frameSignDataProcessor = new Signa.recognizer.FrameSignDataProcessor();
    }

    TrainedSignRecognizer.prototype = {
        _eventEmitter: undefined,
        _frameSignDataProcessor: undefined,
        _signToReconizeId: -1,

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(frame)
        {
            if (!frame.hands.length)
                return;
                
            var data = this._frameSignDataProcessor.extrairFrameDaAmostra(frame);

            Signa.Hubs
                .staticSignRecognizer()
                .recognize(data)
                .then(function(signalRecognizedId)
                {
                    if (signalRecognizedId === this._signToReconizeId)
                    {
                        this._eventEmitter.trigger(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID);
                    }
                }.bind(this));
        },

        setSignToRecognizeId: function(id)
        {
            this._signToReconizeId = id;
        }
    };

    Signa.recognizer.TrainedSignRecognizer = TrainedSignRecognizer;
})(window, window.Signa);