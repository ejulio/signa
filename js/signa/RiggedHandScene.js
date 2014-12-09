;(function(window, Signa, undefined)
{
    function RiggedHandScene(leapController, scene)
    {
        leapController.use('riggedHand', {
            parent: scene.getThreeScene(),
            renderFn: function()
            {
                scene.render();
            }
        }).connect();
    }

    Signa.RiggedHandScene = RiggedHandScene;
})(window, window.Signa);
