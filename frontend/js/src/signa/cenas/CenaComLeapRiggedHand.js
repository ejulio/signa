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
                transparent: true
            },
            renderFn: this.render.bind(this)
        });
    }

    CenaComLeapRiggedHand.prototype = {
        _signaScene: undefined,

        getId: function()
        {
            return this._signaScene.getId();
        },

        render: function()
        {
            this._signaScene.render();
        },

        getThreeScene: function()
        {
            return this._signaScene.getThreeScene();
        },

        getContainer: function()
        {
            return this._signaScene.getContainer();
        }
    };

    Signa.cena.CenaComLeapRiggedHand = CenaComLeapRiggedHand;
})(window, window.Signa);
