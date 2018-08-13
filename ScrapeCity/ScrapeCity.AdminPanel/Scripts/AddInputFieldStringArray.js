//adds more input fields for string arrays
//input fields must be placed inside a table 
//uses table name for input name

let AddInputFieldStringArray = (function () {
    let template = `<tbody>
                        <tr>
                            <td>
                                <input class="form-control rounded-0"
                                       name="{{inputName}}"
                                       type="text"
                                       >
                            </td>
                            <td>
                                <a class="checkbox btn btn-secondary rounded-0"
                                   onclick="AddInputFieldStringArray.RemoveField(this)"
                                   >
                                &times;
                                </a>
                            </td>
                        </tr>
                 </tbody>`;

    let compiledTemplate = Template7.compile(template);

    return {
        AddField: function (button) {
            let table = button.nextElementSibling;
            let tableName = table.attributes["name"].value;
            let context = {
                inputName: tableName
            };
            let html = compiledTemplate(context);
            $(table).append(html);
        },
        RemoveField: function (button) {
            button.parentElement.parentElement.parentElement.remove();
        }
    };
})();
