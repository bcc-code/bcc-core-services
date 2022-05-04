
export default () => {
    const documentationData = process.env.DOCUMENTATION_URLS
    console.log(documentationData)
    if(!documentationData) return []
    return JSON.parse(documentationData)
}
