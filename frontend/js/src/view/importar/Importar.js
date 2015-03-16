;(function(window, View, Signa, undefined)
{
    'use strict';

    function Importar(){}

    Importar.prototype = {
        _leapRecordingPlayer: undefined,
        _framesCarregados: undefined,
        _frameSignDataProcessor: undefined,
        _framesCarregadosEmFormatoJson: undefined,

        iniciar: function()
        {
            var leapController = new Leap.Controller();

            Signa.Hubs.iniciar();
            this._iniciarCena(leapController);

            this._leapRecordingPlayer = new Signa.LeapRecordingPlayer(leapController);
            this._frameSignDataProcessor = new Signa.reconhecimento.InformacoesDoFrame();

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
                acao;

            if (amostra.length === 1) {
                acao = 'SalvarAmostraDeSinalEstatico';
            } else {
                acao = 'SalvarAmostraDeSinalDinamico';
            }
            
            $.post('http://localhost:9000/sinais/' + acao, {
                descricao: descricaoDoSinal,
                conteudoDoArquivoDeExemplo: this._framesCarregadosEmFormatoJson,
                amostra: amostra
            });
        },

        _gerarAmostra: function()
        {
            var framesCarregados = this._framesCarregados,
                amostra = new Array(framesCarregados.length);

            for (var i = 0; i < amostra.length; i++) {
                var frame = new Leap.Frame(this._framesCarregados[0]);
                amostra[i] = this._frameSignDataProcessor.extrairParaAmostra(frame);
            }
            
            return amostra;
        }
    };


    View.importar.Importar = Importar;
})(window, window.View, window.Signa);
