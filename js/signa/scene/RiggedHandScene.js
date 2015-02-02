;(function(window, Signa, undefined)
{
    function RiggedHandScene(leapController, signaScene)
    {
        this._signaScene = signaScene;

        this._riggedHand = new RiggedHand(leapController, {
            sceneid: signaScene.getId(),
            parent: this.getThreeScene(),
            materialOptions: {
                transparent: false
            },
            renderFn: this.render.bind(this)
        });

        leapController.on('frame', this._riggedHand.onFrame);
    }

    RiggedHandScene.prototype = {
        _signaScene: undefined,
        _riggedHand: undefined,

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
