;(function(window, Signa, undefined)
{
    var connection = $.connection,
        recognizerHub = connection.recognizer;

    connection.hub.url = 'http://localhost:9000/signalr';

    // utilizar um hub fake para offline enquanto não inicia o hub
    connection.hub.start();

    function SignalRecognizer(leapController)
    {
        leapController.on('frame', this._onLeapFrame.bind(this));
    }

    SignalRecognizer.prototype = {
        recognize: function(frame)
        {
            //recognizerHub.recognize();
            debugger;
        },

        _onLeapFrame: function(frame)
        {
            // pegar os dados necessários do frame
            //this.recognize(dados);
        }
    };

    Signa.SignalRecognizer = SignalRecognizer;
})(window, window.Signa);
