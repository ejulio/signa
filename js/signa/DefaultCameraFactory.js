;(function(window, Signa, undefined)
{
    var CAMERA_NEAR = 0.1;
    var CAMERA_FAR = 1000;
    var CAMERA_FIELD_OF_VIEW = 75;

    function DefaultCameraFactory(aspectRatio)
    {
        this._aspectRatio = aspectRatio;
    }

    DefaultCameraFactory.prototype = {
        _aspectRatio: 0,

        create: function()
        {
            var camera = new THREE.PerspectiveCamera(CAMERA_FIELD_OF_VIEW, this._aspectRatio, CAMERA_NEAR, CAMERA_FAR);
            camera.position.set(0, 10, 10);
            camera.rotation.set(-0.75, 0, 0);

            return camera;
        }
    };

    Signa.DefaultCameraFactory = DefaultCameraFactory;
})(window, window.Signa);
