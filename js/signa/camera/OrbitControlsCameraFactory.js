;(function(window, Signa, undefined)
{
    function OrbitControlsCameraFactory(cameraFactory)
    {
        this._cameraFactory = cameraFactory;
    }

    OrbitControlsCameraFactory.prototype = {
        _cameraFactory: undefined,
        _orbitControls: undefined,
        _signaScene: undefined,

        create: function()
        {
            var camera = this._cameraFactory.create();
            this._orbitControls = new THREE.OrbitControls(camera);

            return camera;
        },

        _onControlsChange: function()
        {
            this._signaScene.render();
        },

        setSignaScene: function(signaScene)
        {
            this._signaScene = signaScene;
            this._orbitControls.addEventListener('change', this._onControlsChange.bind(this));
        }
    };

    Signa.camera.OrbitControlsCameraFactory = OrbitControlsCameraFactory;
})(window, window.Signa);
