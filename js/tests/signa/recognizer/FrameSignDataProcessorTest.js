var FrameBuilder = require('../../builders/FrameBuilder.js'),
    HandBuilder = require('../../builders/HandBuilder.js'),
    FingerBuilder = require('../../builders/FingerBuilder.js');

global.THREE = require('three');
require('../../../src/signa/Signa.js');
require('../../../src/signa/recognizer/FrameSignDataProcessor.js');

var FrameSignDataProcessor = global.Signa.recognizer.FrameSignDataProcessor;

describe('FrameSignDataProcessor', function()
{
    var HAND_DIRECTION = [1, 2, 3],
        HAND_PALM_NORMAL = [0, 0, 1];

    var frameSignDataProcessor;
    beforeEach(function()
    {
        frameSignDataProcessor = new FrameSignDataProcessor();
    });

    it('is getting sign data from a frame with one hand', function()
    {
        var frame = givenFrameWithOneHand();

        var signData = frameSignDataProcessor.process(frame);

        expect(signData.hands.length).toBe(1);
        expect(signData.hands[0].palmNormal).toBe(HAND_PALM_NORMAL);
        expect(signData.hands[0].handDirection).toBe(HAND_DIRECTION);
        expect(signData.hands[0].anglesBetweenFingers.length).toBe(4);
    });

    it('is getting sign data from a frame with two hands', function()
    {
        var frame = givenFrameWithTwoHands();

        var signData = frameSignDataProcessor.process(frame);

        expect(signData.hands.length).toBe(2);
        expect(signData.hands[0].palmNormal).toBe(HAND_PALM_NORMAL);
        expect(signData.hands[0].handDirection).toBe(HAND_DIRECTION);
        expect(signData.hands[0].anglesBetweenFingers.length).toBe(4);

        expect(signData.hands[1].palmNormal).toBe(HAND_PALM_NORMAL);
        expect(signData.hands[1].handDirection).toBe(HAND_DIRECTION);
        expect(signData.hands[1].anglesBetweenFingers.length).toBe(4);
    });

    function givenFrameWithOneHand()
    {
        var fingers = [
            new FingerBuilder().withTipPosition([1, 1, 1]).build(),
            new FingerBuilder().withTipPosition([2, 2, 2]).build(),
            new FingerBuilder().withTipPosition([3, 3, 3]).build(),
            new FingerBuilder().withTipPosition([4, 4, 4]).build(),
            new FingerBuilder().withTipPosition([5, 5, 5]).build()
        ];

        var hand = new HandBuilder()
            .withDirection(HAND_DIRECTION)
            .withPalmNormal(HAND_PALM_NORMAL)
            .withFingers(fingers)
            .build();

        return new FrameBuilder()
            .withHands([hand])
            .build();
    }

    function givenFrameWithTwoHands()
    {
        var fingers = [
            new FingerBuilder().withTipPosition([1, 1, 1]).build(),
            new FingerBuilder().withTipPosition([2, 2, 2]).build(),
            new FingerBuilder().withTipPosition([3, 3, 3]).build(),
            new FingerBuilder().withTipPosition([4, 4, 4]).build(),
            new FingerBuilder().withTipPosition([5, 5, 5]).build()
        ];

        var hands = [
            new HandBuilder()
                .withDirection(HAND_DIRECTION)
                .withPalmNormal(HAND_PALM_NORMAL)
                .withFingers(fingers)
                .build(),
            new HandBuilder()
                .withDirection(HAND_DIRECTION)
                .withPalmNormal(HAND_PALM_NORMAL)
                .withFingers(fingers)
                .build()
        ];

        return new FrameBuilder()
            .withHands(hands)
            .build();
    }
});
