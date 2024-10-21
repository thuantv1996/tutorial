export const WithCommonProps = (Component) => {
    return (props) => {
        const apiUrls = {
            getList: "/api/items",
            getItem: "/api/item/{id}",
            createItem: "/api/create",
        }
        return <Component {...props} apiUrls={apiUrls} />
    }
}