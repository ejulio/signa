;(function(window, View, Signa, undefined) {
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

        iniciar: function() {
            var largura = $("#handmodel-user").width(),
                altura = $("#handmodel-user").height(),
                cameraFactory = new Signa.camera.DefaultCameraFactory(largura / altura);

            this._messageBox = $('#recognized-sign-message');

            this._descricaoDoSinal = new View.index.ContainerComDescricaoDoSinal($('#sign-description'));
            this._iniciarExemploDoSinal(cameraFactory, largura, altura);
            this._iniciarMaosDoUsuario(cameraFactory, largura, altura);

            this._carregarProximoSinal();
            $('#pular-sinal').click(this._carregarProximoSinal.bind(this));
            $('#treinar-algoritmos').click(Signa.treinarAlgoritmos);
        },

        _iniciarExemploDoSinal: function(cameraFactory, largura, altura) {
            var leapController = new Leap.Controller(),
                container = $("#handmodel-example");

            this._exemploDoSinal = new View.index.ContainerComExemploDoSinal(cameraFactory, container, leapController, largura, altura);

            leapController.on('playback.recordingSet', this._onCarregarNovoSinal.bind(this));
        },

        _iniciarMaosDoUsuario: function(cameraFactory, largura, altura) {
            var leapController = new Leap.Controller(),
                container = $("#handmodel-user"),
                frameBuffer = Signa.frames.FrameBuffer.doLeapController(leapController);

            this._maosDoUsuario = new View.index.ContainerComMaosDoUsuario(cameraFactory, container, leapController, largura, altura);
            
            leapController.connect();

            this._reconhecedorDeSinais = new Signa.reconhecimento.ReconhecedorDeSinais(frameBuffer);
            this._reconhecedorDeSinais.adicionarListenerDeReconhecimento(this._onReconhecer.bind(this));
        },

        _onNovoSinal: function(informacoesDoSinal) {
            this._informacoesDoSinal = informacoesDoSinal;
            this._exemploDoSinal.onNovoSinal(informacoesDoSinal);
        },

        _onCarregarNovoSinal: function() {
            this._descricaoDoSinal.onNovoSinal(this._informacoesDoSinal);
            this._maosDoUsuario.onNovoSinal(this._informacoesDoSinal);
            this._reconhecedorDeSinais.setTipoDoSinal(this._informacoesDoSinal.Tipo);
            this._reconhecedorDeSinais.setIdDoSinalParaReconhecer(this._informacoesDoSinal.Id);
            this._esconderCaixaDeMensagem();
        },

        _esconderCaixaDeMensagem: function() {
            this._messageBox.hide();
        },

        _onReconhecer: function() {
            this._mostrarCaixaDeMensagem();
            this._descricaoDoSinal.onReconhecer();
            this._exemploDoSinal.onReconhecer();
            this._maosDoUsuario.onReconhecer();
            this._reconhecedorDeSinais.setIdDoSinalParaReconhecer(-1);
            window.setTimeout(this._carregarProximoSinal.bind(this), 1000);
        },

        _mostrarCaixaDeMensagem: function() {
            this._messageBox.show();
        },

        _carregarProximoSinal: function() {
            Signa.Hubs
                .iniciar()
                .done(function() {
                    var id = this._informacoesDoSinal ? this._informacoesDoSinal.Id : -1;
                    Signa.Hubs
                        .sinais()
                        .proximoSinal(id)
                        .done(this._onNovoSinal.bind(this));
                }.bind(this));
        }
    };


    View.index.Index = Index;
})(window, window.View, window.Signa);
