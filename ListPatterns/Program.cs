int[] input = new int[] { 1, 2, 4, 8, 16 };

bool result = false;
//var
result = input is [.., var a, var b, _, _];//true


Console.ReadLine();


result = input is [1, 2, 4, 8, 16];//true
result = input is [1, 4, 8, 16];//false
result = input is [1, 4, 8, 2, 16];//false
result = input is [1, 2, 4, 8, 15];//false

//discard
result = input is [_, _, 4, _, _];//true
result = input is [_, _, 4, _];//false
result = input is [_, _, 4, _, 16];//true
result = input is [_, 4, _, _, 16];//false

//range
result = input is [1, ..];//true
result = input is [_, _, 4, ..];//true
result = input is [1, 2, .., 16];//true
