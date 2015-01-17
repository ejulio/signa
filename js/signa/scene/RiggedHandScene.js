;(function(window, Signa, undefined)
{
    function RiggedHandScene(leapController, signaScene)
    {
        var riggedHand = new RiggedHand(leapController, {
            sceneid: 1,
            parent: signaScene.getThreeScene(),
            materialOptions: {
                transparent: false
            },
            renderFn: function()
            {
                signaScene.render();
            }
        });

        leapController.on('frame', riggedHand.onFrame).connect();
    }

    Signa.scene.RiggedHandScene = RiggedHandScene;
})(window, window.Signa);
