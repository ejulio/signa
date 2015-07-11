;(function(window, Signa, undefined)
{
    'use strict';
    
    var idGlobalDeCenas = 0;

    function proximoIdGlobalDeCenas() {
        idGlobalDeCenas++;
        return idGlobalDeCenas;
    }

    function Cena(cameraFactory, container, largura, altura)
    {
        this._id = proximoIdGlobalDeCenas();
        this._scene = new THREE.Scene();
        this._renderer = new THREE.WebGLRenderer();
        this._container = container[0];
        this._camera = cameraFactory.create(this);

        this._renderer.setClearColor(0xF7F7F7);

        this._drawUrs();

        this._renderer.setSize(largura, altura);
        container.append(this._renderer.domElement);
    }

    Cena.prototype = {
        _scene: undefined,
        _camera: undefined,
        _renderer: undefined,
        _container: undefined,
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

        resetCameraPosition: function() {
            this._camera.position.set(0, 250, 250);
            this._camera.rotation.set(-0.5, 0, 0);
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

    Signa.cenas.Cena = Cena;
})(window, window.Signa);
