function(block, generator) {
    var value_cond = generator.valueToCode(block, 'COND', javascript.Order.NONE);
    var statements_if = generator.valueToCode(block, 'IF', javascript.Order.NONE);
    var statements_else = generator.valueToCode(block, 'ELSE', javascript.Order.NONE);
    var code = 'if('+value_cond+')\n{\n'+statements_if+'\n}\nelse\n{\n'+statements_else+'\n}\n';
    return code;
};