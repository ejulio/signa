;(function(window, Signa, undefined)
{
    function RiggedHandScene(leapController, signaScene)
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
        }
    };

    Signa.scene.RiggedHandScene = RiggedHandScene;
})(window, window.Signa);
