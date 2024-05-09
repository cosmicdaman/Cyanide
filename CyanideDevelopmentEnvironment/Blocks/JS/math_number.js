function(block, generator) {
    // Numeric value.
    let number = Number(block.getFieldValue("NUM"))
    if (number === Infinity) {
        return ["INF", javascript.Order.ATOMIC]
    } else if (number === -Infinity) {
        return ["-INF", javascript.Order.UNARY_NEGATION]
    }
    return [String(number), number >= 0 ? javascript.Order.ATOMIC : javascript.Order.UNARY_NEGATION]
}
