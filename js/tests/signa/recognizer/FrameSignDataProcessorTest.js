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

        expect(signData.rightHand).toBeNull();
        mustReturnSignDataWithHandData(signData.leftHand, leftHand);
    });

    it('is getting sign data from a frame with right hand', function()
    {
        var rightHand = givenRightHand();
        var frame = givenFrameWithHands(null, rightHand);

        var signData = frameSignDataProcessor.process(frame);

        expect(signData.leftHand).toBeNull();
        mustReturnSignDataWithHandData(signData.rightHand, rightHand);
    });

    it('is getting sign data from a frame with two hands', function()
    {
        var leftHand = givenLeftHand();
        var rightHand = givenRightHand();
        var frame = givenFrameWithHands(leftHand, rightHand);

        var signData = frameSignDataProcessor.process(frame);

        mustReturnSignDataWithHandData(signData.leftHand, leftHand);
        mustReturnSignDataWithHandData(signData.rightHand, rightHand);
    });

    function givenLeftHand()
    {
        var fingers = [
            new FingerBuilder().withTipPosition([1, 1, 1]).build(),
            new FingerBuilder().withTipPosition([2, 2, 2]).build(),
            new FingerBuilder().withTipPosition([3, 3, 3]).build(),
            new FingerBuilder().withTipPosition([4, 4, 4]).build(),
            new FingerBuilder().withTipPosition([5, 5, 5]).build()
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
            new FingerBuilder().withTipPosition([1, 1, 1]).build(),
            new FingerBuilder().withTipPosition([2, 2, 2]).build(),
            new FingerBuilder().withTipPosition([3, 3, 3]).build(),
            new FingerBuilder().withTipPosition([4, 4, 4]).build(),
            new FingerBuilder().withTipPosition([5, 5, 5]).build()
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
        expect(signDataHand.anglesBetweenFingers.length).toBe(4);
    }
});
