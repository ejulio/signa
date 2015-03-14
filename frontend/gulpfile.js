var gulp = require('gulp'),
    plugins = require('gulp-load-plugins')(),
    connect = require('gulp-connect');

var JAVASCRIPT_CONCATENADO_SRC = 'js/main.js';
var JAVASCRIPT_SRC = ['js/src/signa/**/*.js', 'js/src/view/**/*.js'];

gulp.task('deploy', ['concatenar-js', 'minimizar-js']);

gulp.task('minimizar-js', function() {
    gulp.src(JAVASCRIPT_CONCATENADO_SRC)
        .pipe(plugins.uglify())
        .pipe(gulp.dest('js'));
});

gulp.task('testes', function()
{
    gulp.src('js/tests/**/*.js')
        .pipe(plugins.jasmine());
});

gulp.task('dev', function() {
    connect.server();
    gulp.watch(JAVASCRIPT_SRC, ['concatenar-js', 'lint']);
});

gulp.task('concatenar-js', function() {
    gulp.src(JAVASCRIPT_SRC)
        .pipe(plugins.order([
            'Signa.js',
            'View.js'
        ]))
        .pipe(plugins.concat('main.js'))
        .pipe(gulp.dest('js'));
});

gulp.task('lint', function()
{
    gulp.src(JAVASCRIPT_CONCATENADO_SRC)
        .pipe(plugins.jshint())
        .pipe(plugins.jshint.reporter('jshint-stylish', { verbose: true }));
});