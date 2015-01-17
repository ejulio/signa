;(function(window, View, Signa, undefined)
{
    function SignExample(cameraFactory, container, leapController, width, height)
    {
        var orbitConstrolsCameraFactory = new Signa.camera.OrbitControlsCameraFactory(cameraFactory),
            exampleHandmodelScene = new Signa.scene.Scene(orbitConstrolsCameraFactory, container, width, height);

        orbitConstrolsCameraFactory.setSignaScene(exampleHandmodelScene);

        this._riggedHandPlayer = new Signa.scene.PlaybackRiggedHandScene(leapController, exampleHandmodelScene);
    }

    SignExample.prototype = {
        _riggedHandPlayer: undefined,

        onNewSign: function(signInfo)
        {
            this._riggedHandPlayer.loadRecording('recordings/' + signInfo.ExampleFilePath);
        },

        onRecognize: function()
        {

        }
    };

    View.index.SignExample = SignExample;
})(window, window.View, window.Signa);
