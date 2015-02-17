;(function(window, Signa, undefined)
{
    var globalSceneId = 0;
    function Scene(cameraFactory, container, width, height)
    {
        this._id = globalSceneId;
        globalSceneId++;
        this._scene = new THREE.Scene();
        this._renderer = new THREE.WebGLRenderer();
        this._container = container[0];
        this._camera = cameraFactory.create(this);

        this._drawUrs();

        this._renderer.setSize(width, height);
        container.append(this._renderer.domElement);
    }

    Scene.prototype = {
        _scene: undefined,
        _camera: undefined,
        _renderer: undefined,
        _id: 0,

        getId: function()
        {
            return this._id;
        },

        render: function()
        {
            this._renderer.render(this._scene, this._camera);
        },

        getThreeScene: function()
        {
            return this._scene;
        },

        getContainer: function()
        {
            return this._container;
        },

        _drawUrs: function()
        {
            var lineMaterial,
                lineGeometry,
                line;

            lineMaterial = new THREE.LineBasicMaterial({color: 0x00FF00});
            lineGeometry = new THREE.Geometry();
            lineGeometry.vertices.push(new THREE.Vector3(0, 0, 0));
            lineGeometry.vertices.push(new THREE.Vector3(0, 100, 0));
            line = new THREE.Line(lineGeometry, lineMaterial);
            this._scene.add(line);

            lineMaterial = new THREE.LineBasicMaterial({color: 0xFF0000});
            lineGeometry = new THREE.Geometry();
            lineGeometry.vertices.push(new THREE.Vector3(0, 0, 0));
            lineGeometry.vertices.push(new THREE.Vector3(100, 0, 0));
            line = new THREE.Line(lineGeometry, lineMaterial);
            this._scene.add(line);

            lineMaterial = new THREE.LineBasicMaterial({color: 0x0000FF});
            lineGeometry = new THREE.Geometry();
            lineGeometry.vertices.push(new THREE.Vector3(0, 0, 0));
            lineGeometry.vertices.push(new THREE.Vector3(0, 0, 100));
            line = new THREE.Line(lineGeometry, lineMaterial);
            this._scene.add(line);
        }
    };

    Signa.scene.Scene = Scene;
})(window, window.Signa);
