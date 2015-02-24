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

        onNewSign: function(signInfo)
        {
            this._leapRecordingPlayer.loadRecording('http://localhost:9000/' + signInfo.ExampleFilePath);
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
