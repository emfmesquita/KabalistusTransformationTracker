/// <binding AfterBuild='copy' ProjectOpened='watch' />
module.exports = function (grunt) {

    // Project configuration.
    grunt.initConfig({
        app: {
            nodedir: "node_modules",
            appdir: "Resources",
            target: "bin/Debug/Resources"
        },
        watch: {
            files: [
                "<%= app.appdir %>/**"
            ],
            tasks: [
                "copy:app"
            ]
        },
        copy: {
            ext: {
                files: [
				  { expand: true, cwd: "<%= app.nodedir %>/jquery/dist", src: ["jquery.js"], dest: "<%= app.target %>/scripts" },
                ]
            },
            app: {
                files: [
				  { expand: true, cwd: "<%= app.appdir %>/Scripts", src: ["*.js"], dest: "<%= app.target %>/scripts" },
				  { expand: true, cwd: "<%= app.appdir %>", src: ["index.html"], dest: "<%= app.target %>" }
                ]
            }
        }

    });

    grunt.loadNpmTasks("grunt-contrib-copy");
    grunt.loadNpmTasks("grunt-contrib-watch");

    // Default task(s).
    grunt.registerTask("default", ["copy"]);

};