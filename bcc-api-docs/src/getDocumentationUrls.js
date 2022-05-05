
export default () => {
    const documentationUrls = process.env.DOCUMENTATION_URLS
    if(!documentationUrls) return []
    return JSON.parse(documentationUrls)
}
