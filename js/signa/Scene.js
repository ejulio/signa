;(function(window, Signa, undefined)
{
    var CAMERA_NEAR = 0.1;
    var CAMERA_FAR = 1000;
    var CAMERA_FIELD_OF_VIEW = 75;

    function Scene(container, width, height)
    {
        var aspectRatio = width / height;
        this._scene = new THREE.Scene();
        this._camera = new THREE.PerspectiveCamera(CAMERA_FIELD_OF_VIEW, aspectRatio, CAMERA_NEAR, CAMERA_FAR);
        this._renderer = new THREE.WebGLRenderer();

        this._camera.position.z = 20;
        this._camera.position.y = 10;
        this._camera.rotation.x = -0.15;

        this._drawUrs();

        this._renderer.setSize(width, height);
        container.append(this._renderer.domElement);
    }

    Scene.prototype = {
        _scene: undefined,
        _camera: undefined,
        _renderer: undefined,

        render: function()
        {
            this._renderer.render(this._scene, this._camera);
        },

        getThreeScene: function()
        {
            return this._scene;
        },

        _drawUrs: function()
        {
            var lineMaterial,
                lineGeometry,
                line;

            lineMaterial = new THREE.LineBasicMaterial({color: 0xFF0000});
            lineGeometry = new THREE.Geometry();
            lineGeometry.vertices.push(new THREE.Vector3(0, 0, 0));
            lineGeometry.vertices.push(new THREE.Vector3(0, 20, 0));
            line = new THREE.Line(lineGeometry, lineMaterial);
            this._scene.add(line);

            lineMaterial = new THREE.LineBasicMaterial({color: 0x00FF00});
            lineGeometry = new THREE.Geometry();
            lineGeometry.vertices.push(new THREE.Vector3(0, 0, 0));
            lineGeometry.vertices.push(new THREE.Vector3(20, 0, 0));
            line = new THREE.Line(lineGeometry, lineMaterial);
            this._scene.add(line);

            lineMaterial = new THREE.LineBasicMaterial({color: 0x0000FF});
            lineGeometry = new THREE.Geometry();
            lineGeometry.vertices.push(new THREE.Vector3(0, 0, 0));
            lineGeometry.vertices.push(new THREE.Vector3(0, 0, 20));
            line = new THREE.Line(lineGeometry, lineMaterial);
            this._scene.add(line);
        }
    };

    Signa.Scene = Scene;
})(window, window.Signa);
