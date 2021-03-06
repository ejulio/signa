;(function(window, View, Signa, undefined) {
    'use strict';
    
    function ContainerComExemploDoSinal(cameraFactory, container, leapController, largura, altura) {
        var cameraComControlesFactory = new Signa.camera.OrbitControlsCameraFactory(cameraFactory);
        this._cena = Signa.cenas.CenaFactory.criarCenaComLeapRiggedHand(largura, altura, container, cameraComControlesFactory, leapController);

        this._leapRecordingPlayer = new Signa.LeapRecordingPlayer(leapController);

        $('#play-pause').click(this._onPlayPause.bind(this));
    }

    ContainerComExemploDoSinal.prototype = {
        _leapRecordingPlayer: undefined,
        _cena: undefined,

        onNovoSinal: function(informacoesDoSinal) {
            this._cena.resetCameraPosition();
            this._leapRecordingPlayer.loadRecording('http://localhost:9000/' + informacoesDoSinal.CaminhoParaArquivoDeExemplo);
        },

        onReconhecer: function() { 
            
        },

        _onPlayPause: function() {
            this._leapRecordingPlayer.toggle();
        }
    };

    View.index.ContainerComExemploDoSinal = ContainerComExemploDoSinal;
})(window, window.View, window.Signa);
