
export default () => {
    const documentationData = process.env.DOCUMENTATION_DATA
    console.log(documentationData)
    if(!documentationData) return []
    return JSON.parse(documentationData)
}
