;(function(window, Signa, undefined)
{
    'use strict';
    
    function OrbitControlsCameraFactory(cameraFactory)
    {
        this._cameraFactory = cameraFactory;
    }

    OrbitControlsCameraFactory.prototype = {
        _cameraFactory: undefined,
        _orbitControls: undefined,
        _signaScene: undefined,

        create: function(signaScene)
        {
            this._signaScene = signaScene;

            var camera = this._cameraFactory.create();
            this._orbitControls = new THREE.OrbitControls(camera, signaScene.getContainer());
            this._orbitControls.addEventListener('change', this._onControlsChange.bind(this));

            return camera;
        },

        _onControlsChange: function()
        {
            this._signaScene.render();
        }
    };

    Signa.camera.OrbitControlsCameraFactory = OrbitControlsCameraFactory;
})(window, window.Signa);
