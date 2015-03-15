;(function(window, Signa, undefined) {
    'use strict';

    function TrainedSignRecognizer(eventEmitter) {
        this._eventEmitter = eventEmitter;
        this._informacoesDoFrame = new Signa.reconhecimento.InformacoesDoFrame();
    }

    TrainedSignRecognizer.prototype = {
        _eventEmitter: undefined,
        _informacoesDoFrame: undefined,
        _signToReconizeId: -1,
        _algoritmo: undefined,

        addRecognizeEventListener: function(listener) {
            this._eventEmitter.addListener(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(frame) {
            if (!frame.hands.length)
                return;
                
            var dados = this._informacoesDoFrame.extrairParaAmostra(frame);
            this._algoritmo.reconhecer(dados);
        },

        setSignToRecognizeId: function(id) {
            this._signToReconizeId = id;
            this._algoritmo.setSinalId(id);
        },

        setTipoDoSinal: function(tipoDoSinal) {
            if (this._ehSinalEstatico(tipoDoSinal)) {
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
