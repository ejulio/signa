var FrameBuilder = require('../../builders/FrameBuilder.js'),
    HandBuilder = require('../../builders/HandBuilder.js'),
    FingerBuilder = require('../../builders/FingerBuilder.js');

global.THREE = require('three');
require('../../../src/signa/Signa.js');
require('../../../src/signa/recognizer/FrameSignDataProcessor.js');

var FrameSignDataProcessor = global.Signa.recognizer.FrameSignDataProcessor;

describe('FrameSignDataProcessor', function()
{
    var LEFT_HAND_DIRECTION = [1, 2, 3],
        LEFT_HAND_PALM_NORMAL = [0, 0, 1],
        RIGHT_HAND_DIRECTION = [4, 5, 6],
        RIGHT_HAND_PALM_NORMAL = [1, 1, 0];

    var frameSignDataProcessor;
    beforeEach(function()
    {
        frameSignDataProcessor = new FrameSignDataProcessor();
    });

    it('is getting sign data from a frame with left hand', function()
    {
        var leftHand = givenLeftHand();
        var frame = givenFrameWithHands(leftHand, null);

        var signData = frameSignDataProcessor.process(frame);

        expect(signData.RightHand).toBeNull();
        mustReturnSignDataWithHandData(signData.LeftHand, leftHand);
    });

    it('is getting sign data from a frame with right hand', function()
    {
        var rightHand = givenRightHand();
        var frame = givenFrameWithHands(null, rightHand);

        var signData = frameSignDataProcessor.process(frame);

        expect(signData.LeftHand).toBeNull();
        mustReturnSignDataWithHandData(signData.RightHand, rightHand);
    });

    it('is getting sign data from a frame with two hands', function()
    {
        var leftHand = givenLeftHand();
        var rightHand = givenRightHand();
        var frame = givenFrameWithHands(leftHand, rightHand);

        var signData = frameSignDataProcessor.process(frame);

        mustReturnSignDataWithHandData(signData.LeftHand, leftHand);
        mustReturnSignDataWithHandData(signData.RightHand, rightHand);
    });

    function givenLeftHand()
    {
        var fingers = [
            FingerBuilder.thumb(),
            FingerBuilder.index(),
            FingerBuilder.middle(),
            FingerBuilder.ring(),
            FingerBuilder.pinky()
        ];

        return new HandBuilder()
            .withDirection(LEFT_HAND_DIRECTION)
            .withPalmNormal(LEFT_HAND_PALM_NORMAL)
            .withFingers(fingers)
            .build();
    }

    function givenRightHand()
    {
        var fingers = [
            FingerBuilder.thumb(),
            FingerBuilder.index(),
            FingerBuilder.middle(),
            FingerBuilder.ring(),
            FingerBuilder.pinky()
        ];

        return new HandBuilder()
            .withDirection(RIGHT_HAND_DIRECTION)
            .withPalmNormal(RIGHT_HAND_PALM_NORMAL)
            .withFingers(fingers)
            .build();
    }

    function givenFrameWithHands(leftHand, rightHand)
    {
        return new FrameBuilder()
            .withLeftHand(leftHand)
            .withRightHand(rightHand)
            .build();
    }

    function mustReturnSignDataWithHandData(signDataHand, hand)
    {
        expect(signDataHand).not.toBeNull();
        expect(signDataHand.palmNormal).toBe(hand.palmNormal);
        expect(signDataHand.handDirection).toBe(hand.direction);
        
        var fingers = signDataHand.fingers;        
        for (var i = 0; i < fingers.length; i++)
        {
            expect(fingers[i].type).toBe(hand.fingers[i].type);
            expect(fingers[i].direction).toBe(hand.fingers[i].direction);
        }
    }
});
