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
