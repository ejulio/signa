;(function(global, Signa, undefined)
{
    'use strict';

    function FrameSignDataProcessor(){}
    FrameSignDataProcessor.prototype = {
        process: function(frame)
        {
            return {
                LeftHand: this._getLeftHandFromFrame(frame.hands),
                RightHand: this._getRightHandFromFrame(frame.hands)
            };
        },

        _getLeftHandFromFrame: function(hands)
        {
            if (this._isLeftHand(hands[0]))
                return this._getHandData(hands[0]);

            if (this._isLeftHand(hands[1]))
                return this._getHandData(hands[1]);

            return null;
        },

        _isLeftHand: function(hand)
        {
            return hand && hand.type.toUpperCase() === 'LEFT';
        },

        _getRightHandFromFrame: function(hands)
        {
            if (this._isRightHand(hands[0]))
                return this._getHandData(hands[0]);

            if (this._isRightHand(hands[1]))
                return this._getHandData(hands[1]);

            return null;
        },

        _isRightHand: function(hand)
        {
            return hand && hand.type.toUpperCase() === 'RIGHT';
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
                PalmNormal: leapHand.palmNormal,
                HandDirection: leapHand.direction,
                AnglesBetweenFingers: anglesBetweenFingers
            };
        }
    };

    Signa.recognizer.FrameSignDataProcessor = FrameSignDataProcessor;
})(global = typeof global === 'undefined' ? window : global, global.Signa);
