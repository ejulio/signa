;(function(window, Signa, undefined) {
    'use strict';

    function TrainedSignRecognizer(eventEmitter) {
        this._eventEmitter = eventEmitter;
        this._informacoesDoFrame = new Signa.reconhecimento.InformacoesDoFrame();
    }

    TrainedSignRecognizer.prototype = {
        _eventEmitter: undefined,
        _informacoesDoFrame: undefined,
        _idDoSinalParaReconhecer: -1,
        _algoritmo: undefined,
        _idDoDelayDeReconhecimento: -1,

        addRecognizeEventListener: function(listener) {
            this._eventEmitter.addListener(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(frame) {
            if (!frame.hands.length)
                return;
            
            if (this._idDoSinalParaReconhecer === -1)
                return;

            if (this._idDoDelayDeReconhecimento === -1) {
                this._idDoDelayDeReconhecimento = window.setTimeout((function(frame) {
                    return function() {
                        var dados = this._informacoesDoFrame.extrairParaAmostra(frame);
                        this._algoritmo
                            .reconhecer(dados)
                            .then(function(sinalFoiReconhecido) {
                                if (sinalFoiReconhecido) {
                                    this._idDoSinalParaReconhecer = -1;
                                    this._eventEmitter.trigger(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID);
                                }
                                this._idDoDelayDeReconhecimento = -1;
                            }.bind(this));
                        }.bind(this);
                }.bind(this))(frame), 500);
            }
        },

        setSignToRecognizeId: function(id) {
            this._idDoSinalParaReconhecer = id;
            this._algoritmo.setSinalId(id);
        },

        setTipoDoSinal: function(tipoDoSinal) {
            if (this._ehSinalEstatico(tipoDoSinal)) {
                console.log('SINAL ESTÁTICO');
                this._algoritmo = new Signa.reconhecimento.AlgoritmoDeSinalEstatico();
            } else {
                console.log('SINAL DINÂMICO');
                this._algoritmo = new Signa.reconhecimento.AlgoritmoDeSinalDinamico();
            }
        },

        _ehSinalEstatico: function(tipoDoSinal) {
            return tipoDoSinal === 0;
        }
    };

    Signa.recognizer.TrainedSignRecognizer = TrainedSignRecognizer;
})(window, window.Signa);
