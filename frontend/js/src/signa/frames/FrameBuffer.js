;(function(window, Signa, undefined) {
    'use strict';
    
    var ID_EVENTO_FRAME = 'frame';
    var INDICE_DO_FRAME_QUE_DEVE_SER_ARMAZENADO = 3;
    var QUANTIDADE_DE_FRAMES_DO_BUFFER = 5;

    function FrameBuffer() {
        this._eventEmitter = new EventEmitter();
        this._indice = 0;
    }

    FrameBuffer.prototype = {
        _indice: undefined,
        _frame: undefined,
        _eventEmitter: undefined,

        adicionarListenerDeFrame: function(callback) {
            this._eventEmitter.addListener(ID_EVENTO_FRAME, callback);
        },

        onFrame: function(frame) {
            if (this._frameEhValido(frame)) {
                this._indice++;
                if (this._deveArmazenarFrameDoIndice()) {
                    this._frame = frame;
                } else if (this._alcancouMaximoDeFrames()) {
                    this._eventEmitter.trigger(ID_EVENTO_FRAME, [this._frame]);
                    this._indice = 0;
                    this._frame = undefined;
                }
            }
        },

        _frameEhValido: function(frame) {
            return frame.hands.length > 0;
        },

        _deveArmazenarFrameDoIndice: function() {
            return this._indice === INDICE_DO_FRAME_QUE_DEVE_SER_ARMAZENADO;
        },

        _alcancouMaximoDeFrames: function() {
            return this._indice === QUANTIDADE_DE_FRAMES_DO_BUFFER;
        }
    };

    FrameBuffer.doLeapController = function(leapController) {
        var frameBuffer = new FrameBuffer();
        leapController.on('frame', frameBuffer.onFrame.bind(frameBuffer));
        return frameBuffer;
    };


    Signa.frames.FrameBuffer = FrameBuffer;
})(global = typeof global === 'undefined' ? window : global, global.Signa);
