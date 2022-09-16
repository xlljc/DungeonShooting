
let args = [
    2,//正数
    -2,//负数
    0,//0
    2.5,//小数
    -2.5,//负小数
    "abc",//字符串
    true,//true
    false,//false
    null,//空对象
    {x: 1, y: 1},//对象
    function() {},//函数
];

const arg1List = [...args];
const arg2List = [...args];

let message = "";

function printMessage(arg1, arg2, symbolStr, result) {
    message += (`\n${arg1} ${symbolStr} ${arg2} => ${result}   ------   ${typeof arg1} ${symbolStr} ${typeof arg2} => ${typeof result}`);
}

function printMessageSingle(arg1, symbolStr, result) {
    message += (`\n${arg1} ${symbolStr} => ${result}   ------   ${typeof arg1} ${symbolStr} => ${typeof result}`);
}

message += (`\n
------------------------------------------------------------------
                         加法
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    for (let j = 0; j < arg2List.length; j++) {
        let a2 = arg2List[j];
        printMessage(a1, a2, "+", a1 + a2);
    }
}

message += (`\n
------------------------------------------------------------------
                         减法
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    for (let j = 0; j < arg2List.length; j++) {
        let a2 = arg2List[j];
        printMessage(a1, a2, "-", a1 - a2);
    }
}


message += (`\n
------------------------------------------------------------------
                         乘法
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    for (let j = 0; j < arg2List.length; j++) {
        let a2 = arg2List[j];
        printMessage(a1, a2, "*", a1 * a2);
    }
}


message += (`\n
------------------------------------------------------------------
                         除法
------------------------------------------------------------------
`);

for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    for (let j = 0; j < arg2List.length; j++) {
        let a2 = arg2List[j];
        printMessage(a1, a2, "/", a1 / a2);
    }
}

message += (`\n
------------------------------------------------------------------
                         取模
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    for (let j = 0; j < arg2List.length; j++) {
        let a2 = arg2List[j];
        printMessage(a1, a2, "%", a1 % a2);
    }
}

message += (`\n
------------------------------------------------------------------
                         二进制左移
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    for (let j = 0; j < arg2List.length; j++) {
        let a2 = arg2List[j];
        printMessage(a1, a2, "<<", a1 << a2);
    }
}

message += (`\n
------------------------------------------------------------------
                         二进制右移
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    for (let j = 0; j < arg2List.length; j++) {
        let a2 = arg2List[j];
        printMessage(a1, a2, ">>", a1 >> a2);
    }
}

message += (`\n
------------------------------------------------------------------
                         二进制与
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    for (let j = 0; j < arg2List.length; j++) {
        let a2 = arg2List[j];
        printMessage(a1, a2, "&", a1 & a2);
    }
}


message += (`\n
------------------------------------------------------------------
                         二进制或
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    for (let j = 0; j < arg2List.length; j++) {
        let a2 = arg2List[j];
        printMessage(a1, a2, "|", a1 | a2);
    }
}

message += (`\n
------------------------------------------------------------------
                         二进制异或
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    for (let j = 0; j < arg2List.length; j++) {
        let a2 = arg2List[j];
        printMessage(a1, a2, "^", a1 ^ a2);
    }
}

message += (`\n
------------------------------------------------------------------
                         自加
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    printMessageSingle(a1, "++", a1++);
}

message += (`\n
------------------------------------------------------------------
                         自减
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    printMessageSingle(a1, "--", a1--);
}

message += (`\n
------------------------------------------------------------------
                         正数
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    printMessageSingle(a1, "+", +a1);
}


message += (`\n
------------------------------------------------------------------
                         负数
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    printMessageSingle(a1, "-", -a1);
}

message += (`\n
------------------------------------------------------------------
                         取反
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    printMessageSingle(a1, "!", !a1);
}


message += (`\n
------------------------------------------------------------------
                         二进制非运算
------------------------------------------------------------------
`);
for (let i = 0; i < arg1List.length; i++) {
    let a1 = arg1List[i];
    printMessageSingle(a1, "~", ~a1);
}

console.log(message);