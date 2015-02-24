var gulp = require('gulp'),
    plugins = require('gulp-load-plugins')();

var JAVASCRIPT_SRC = ['js/src/*.js', 'js/src/signa/**/*.js', 'js/src/view/**/*.js'];

gulp.task('lint', function()
{
    gulp.src(JAVASCRIPT_SRC)
        .pipe(plugins.jshint())
        .pipe(plugins.jshint.reporter('jshint-stylish', { verbose: true }));
});

gulp.task('tests', function()
{
    gulp.src('js/tests/**/*.js')
        .pipe(plugins.jasmine());
});
