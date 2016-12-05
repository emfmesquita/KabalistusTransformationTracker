(function () {
    const updateTransformation = function (transDiv, transformation) {
        transDiv.find(".transname").text(transformation.name);
        transDiv.find(".transcount").text(transformation.count);
        transDiv.find(".touched").text(transformation.toucheditems.join(", "));
        transDiv.find(".blacklisted").text(transformation.blacklisteditems.join(", "));
        transDiv.find(".missing").text(transformation.missingitems.join(", "));
    }

    const createStructure = function (transformation) {
        const template = $("#template").clone();
        template.css("display", "block");
        template.attr("id", "");
        template.addClass(transformation.class);
        updateTransformation(template, transformation);
        return template;
    }

    const createStructures = function (root, transformations) {
        transformations.forEach((transformation) => root.append(createStructure(transformation)));
    }

    const updateJson = function (transformations) {
        const root = $("#root");

        if (root.children().length === 0) {
            createStructures(root, transformations);
            return;
        }

        transformations.forEach((transformation) => {
            var transDiv = root.find("." + transformation.class);
            updateTransformation(transDiv, transformation);
        });
    }

    const update = function () {
        $.getJSON("/status", {}, (result) => updateJson(result));
    }

    setInterval(update, 1000);
})();