//use like this:
var o = new OneNoteOCR();
var arr = o.OcrTexts("put here the path to the file").ToArray();
foreach(var item in arr){
	var text=item.Text;
}
