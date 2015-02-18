;(function(global, Signa, undefined)
{
    'use strict';

    function FrameSignDataProcessor(){}
    FrameSignDataProcessor.prototype = {
        process: function(frame)
        {
            var hands = new Array(frame.hands.length);

            for (var i = 0; i < hands.length; i++)
            {
                hands[i] = this._getHandData(frame.hands[i]);
            }

            return {
                hands: hands
            };
        },

        _getHandData: function(leapHand)
        {
            var anglesBetweenFingers = [],
                length = leapHand.fingers.length - 1;

            for (var i = 0; i < length; i++)
            {
                var origin = new THREE.Vector3();
                var destiny = new THREE.Vector3();

                origin.fromArray(leapHand.fingers[i].tipPosition);
                destiny.fromArray(leapHand.fingers[i + 1].tipPosition);

                anglesBetweenFingers.push(origin.angleTo(destiny));
            }

            return {
                palmNormal: leapHand.palmNormal,
                handDirection: leapHand.direction,
                anglesBetweenFingers: anglesBetweenFingers
            };
        }
    };

    Signa.recognizer.FrameSignDataProcessor = FrameSignDataProcessor;
})(global = typeof global === 'undefined' ? window : global, global.Signa);
