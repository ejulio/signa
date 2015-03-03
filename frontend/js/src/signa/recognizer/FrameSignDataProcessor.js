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
            return {
                palmNormal: leapHand.palmNormal,
                handDirection: leapHand.direction,
                fingers: this._getHandFingersData(leapHand.fingers)
            };
        },

        _getHandFingersData: function(leapFingers)
        {
            var fingers = new Array(leapFingers.length);

            for (var i = 0; i < fingers.length; i++)
            {
                fingers[i] = {
                    type: leapFingers[i].type,
                    direction: leapFingers[i].direction
                };
            }

            return fingers;
        }
    };

    Signa.recognizer.FrameSignDataProcessor = FrameSignDataProcessor;
})(global = typeof global === 'undefined' ? window : global, global.Signa);
