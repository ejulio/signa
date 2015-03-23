var FrameBuilder = require('../../builders/FrameBuilder.js');

require('../../../src/signa/Signa.js');
require('../../../src/signa/frames/FrameBuffer.js');
global.EventEmitter = require('wolfy87-eventemitter');

describe('FrameBuffer', function()
{
    var frameBuffer;
    beforeEach(function()
    {
        frameBuffer = new Signa.frames.FrameBuffer();
    });

    it('recebendo cinco frames', function()
    {
        var frames = dadoUmArrayComFrames(5);

        var onFrame = jasmine.createSpy('onFrame');

        frameBuffer.adicionarListenerDeFrame(onFrame);

        frames.forEach(function(frame) {
            frameBuffer.onFrame(frame);
        });

        expect(onFrame).toHaveBeenCalledWith(frames[2]);
    });

    it('recebendo dez frames', function()
    {
        var frames = dadoUmArrayComFrames(10);
        var onFrame = jasmine.createSpy('onFrame');

        frameBuffer.adicionarListenerDeFrame(onFrame);

        frames.forEach(function(frame) {
            frameBuffer.onFrame(frame);
        });

        expect(onFrame).toHaveBeenCalledWith(frames[2]);
        expect(onFrame).toHaveBeenCalledWith(frames[7]);
    });

    it('recebendo frames sem maos', function()
    {
        var frames = [
            new FrameBuilder().construir(),
            new FrameBuilder().construir(),
            new FrameBuilder().comMaoDireita(null).comMaoEsquerda(null).construir(),
            new FrameBuilder().construir(),
            new FrameBuilder().construir()
        ];
        var onFrame = jasmine.createSpy('onFrame');

        frameBuffer.adicionarListenerDeFrame(onFrame);

        frames.forEach(function(frame) {
            frameBuffer.onFrame(frame);
        });

        expect(onFrame).not.toHaveBeenCalled();
    });

    function dadoUmArrayComFrames(quantidade) {
        var frames = new Array(quantidade);

        for (var i = 0; i < quantidade; i++) {
            frames[i] = new FrameBuilder().construir();
        }

        return frames;
    }
});
