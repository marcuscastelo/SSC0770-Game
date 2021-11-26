public enum DialogButtonCombination
{
    OK=DialogButton.OK,                                                     // 0b0101
    // Cancel=DialogButton.Cancel,                                             // 0b0110
    OKCancel=DialogButton.OK | DialogButton.Cancel,                         // 0b0111
    // Yes=DialogButton.Yes,                                                   // 0b1001
    // No=DialogButton.No,                                                     // 0b1010
    YesNo=DialogButton.Yes | DialogButton.No,                               // 0b1011
    YesNoCancel=DialogButton.Yes | DialogButton.No | DialogButton.Cancel,   // 0b1111
}