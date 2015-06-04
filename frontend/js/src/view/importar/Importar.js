;(function(window, View, Signa, undefined) {
    'use strict';

    function Importar(){}

    Importar.prototype = {
        _leapRecordingPlayer: undefined,
        _framesCarregados: undefined,
        _informacoesDoFrame: undefined,
        _framesCarregadosEmFormatoJson: undefined,

        iniciar: function() {
            var leapController = new Leap.Controller();

            Signa.Hubs.iniciar();
            this._iniciarCena(leapController);

            this._leapRecordingPlayer = new Signa.LeapRecordingPlayer(leapController);
            this._informacoesDoFrame = new Signa.frames.InformacoesDoFrame();

            $('#message').hide();
            $('#sign-file').change(this._onArquivoDoSinalChange.bind(this));
            $('#save').click(this._onSalvarClick.bind(this));
            $('#treinar-algoritmos').click(Signa.treinarAlgoritmos);
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
            if (this._framesCarregados.length < 5) {
                return this._gerarAmostraEstatica();
            }

            return this._gerarAmostraDinamica();
        },

        _gerarAmostraEstatica: function() {
            var frame = new Leap.Frame(this._framesCarregados[0]),
                frameDaAmostra = this._informacoesDoFrame.extrairParaAmostra(frame);

            return [frameDaAmostra];
        },

        _gerarAmostraDinamica: function() {
            var framesCarregados = this._framesCarregados,
                amostra = [],
                frameBuffer = new Signa.frames.FrameBuffer();

            frameBuffer.adicionarListenerDeFrame(function(frame) {
                var leapFrame = new Leap.Frame(frame),
                    frameDaAmostra = this._informacoesDoFrame.extrairParaAmostra(leapFrame);

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
