export const MainLayout = ({ children }) => {
    return <div className="main-layout">
        <div className="wrap">
            {/* header  */}
            <header className="main-layout__header">
                <div><a href="/">React Js tutorial</a></div>
                <nav>
                    <a href="/">Home</a>
                    <a href="/login">Login</a>
                </nav>
            </header>

            {/* body */}
            <div className="main-layout__body">{children}</div>

            {/* footer */}
            <div className="main-layout__footer">
                <h3>React tutorial</h3>
                <div>This is footer</div>
            </div>
        </div>
    </div>
}