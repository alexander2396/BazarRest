var gulp = require("gulp"),
    fs = require("fs"),
    sass = require("gulp-sass"); 

gulp.task("sass", function () {
    return gulp.src('wwwroot/sass/main.scss')
        .pipe(sass())
        .pipe(gulp.dest('wwwroot/css'));
});

gulp.task('sass:watch', function () {
    gulp.watch('wwwroot/sass/*', ['sass']);
});
