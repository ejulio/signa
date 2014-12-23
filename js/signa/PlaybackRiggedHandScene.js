;(function(window, Signa, undefined)
{
    function PlaybackRiggedHandScene(leapController, signaScene)
    {
        this._leapController = leapController;
        var riggedHand = new RiggedHand(leapController, {
            sceneid: 2,
            parent: signaScene.getThreeScene(),
            materialOptions: {
                transparent: false
            },
            renderFn: function()
            {
                signaScene.render();
            }
        });

        leapController
            .use('playback', {
                overlay: false
            })
            .on('frame', riggedHand.onFrame);

        this._player = leapController.plugins.playback.player;
    }

    PlaybackRiggedHandScene.prototype = {
        _leapController: undefined,
        _player: undefined,

        loadRecording: function(url)
        {
            this._player.setRecording({
                url: url
            });
        },

        toggle: function()
        {
            this._player.toggle();
        }
    };

    Signa.PlaybackRiggedHandScene = PlaybackRiggedHandScene;
})(window, window.Signa);
