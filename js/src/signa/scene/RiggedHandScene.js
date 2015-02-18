;(function(window, Signa, undefined)
{
    'use strict';
    
    function RiggedHandScene(leapController, signaScene)
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

    RiggedHandScene.prototype = {
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

    Signa.scene.RiggedHandScene = RiggedHandScene;
})(window, window.Signa);
