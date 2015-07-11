;(function(window, Signa, undefined)
{
    'use strict';
    
    function CenaComLeapRiggedHand(leapController, signaScene)
    {
        this._signaScene = signaScene;

        leapController.use('riggedHand', {
            sceneId: signaScene.getId(),
            parent: this.getThreeScene(),
            materialOptions: {
                transparent: false
            },
            renderFn: this.render.bind(this)
        });

        var cena = signaScene.getThreeScene();
        var luzAmbiente = new THREE.AmbientLight(0x7b5d2c);
        cena.add(luzAmbiente);

        var luz = new THREE.SpotLight(0xFFFFFF);
        luz.position.set(0, 0, 0);
        luz.target = signaScene._camera;
        cena.add(luz);
    
        var luz2 = new THREE.SpotLight(0xffffff);
        this.luz = luz2;
        cena.add(luz2);
    }

    CenaComLeapRiggedHand.prototype = {
        _signaScene: undefined,

        getId: function()
        {
            return this._signaScene.getId();
        },

        render: function()
        {
            this.luz.position.copy(this._signaScene._camera.position);
            this._signaScene.render();
        },

        getThreeScene: function()
        {
            return this._signaScene.getThreeScene();
        },

        getContainer: function()
        {
            return this._signaScene.getContainer();
        },

        resetCameraPosition: function() {
            this._signaScene.resetCameraPosition();
        }
    };

    Signa.cenas.CenaComLeapRiggedHand = CenaComLeapRiggedHand;
})(window, window.Signa);
