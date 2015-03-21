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
            var userHandsLeapController = new Leap.Controller(),
                container = $("#handmodel-user");

            this._maosDoUsuario = new View.index.ContainerComMaosDoUsuario(cameraFactory, container, userHandsLeapController, width, height);
            
            userHandsLeapController.connect();
            
            this._reconhecedorDeSinais = new Signa.reconhecimento.ReconhecedorDeSinais(userHandsLeapController);
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
