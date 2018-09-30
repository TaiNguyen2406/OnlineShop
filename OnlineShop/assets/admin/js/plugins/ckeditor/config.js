/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
     config.uiColor = '#AADC6E';

    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'en';
    config.filebrowserBrowseUrl = '/assets/admin/js/plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/assets/admin/js/plugins/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/assets/admin/js/plugins/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/assets/admin/js/plugins/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserFlashUploadUrl = '/assets/admin/js/plugins/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Data';

    CKFinder.setupCKEditor(null, '/assets/admin/js/plugins/ckfinder/');
};
