;(function(window, View, Signa, undefined)
{
    function SignExample(cameraFactory, container, leapController, width, height)
    {
        var orbitConstrolsCameraFactory = new Signa.camera.OrbitControlsCameraFactory(cameraFactory),
            exampleHandmodelScene = new Signa.scene.Scene(orbitConstrolsCameraFactory, container, width, height);

        exampleHandmodelScene = new Signa.scene.RiggedHandScene(leapController, exampleHandmodelScene);
        
        this._riggedHandPlayer = new Signa.scene.PlaybackRiggedHandScene(leapController);

        $('#play-pause').click(this._onPlayPause.bind(this));
    }

    SignExample.prototype = {
        _riggedHandPlayer: undefined,

        onNewSign: function(signInfo)
        {
            this._riggedHandPlayer.loadRecording('http://localhost:9000/' + signInfo.ExampleFilePath);
        },

        onRecognize: function()
        {

        },

        _onPlayPause: function()
        {
            this._riggedHandPlayer.toggle();
        }
    };

    View.index.SignExample = SignExample;
})(window, window.View, window.Signa);
