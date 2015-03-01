;(function(window, Signa, undefined)
{
    'use strict';
    
    var CAMERA_NEAR = 0.1;
    var CAMERA_FAR = 1000;
    var CAMERA_FIELD_OF_VIEW = 75;

    function DefaultCameraFactory(aspectRatio)
    {
        this._aspectRatio = aspectRatio;
    }

    DefaultCameraFactory.prototype = {
        _aspectRatio: 0,

        create: function(signaScene)
        {
            var camera = new THREE.PerspectiveCamera(CAMERA_FIELD_OF_VIEW, this._aspectRatio, CAMERA_NEAR, CAMERA_FAR);
            camera.position.set(0, 300, 300);
            camera.rotation.set(-0.5, 0, 0);

            return camera;
        }
    };

    Signa.camera.DefaultCameraFactory = DefaultCameraFactory;
})(window, window.Signa);
