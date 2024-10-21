import { useState } from "react"
import { WithCommonProps } from "../../common/withCommonProps"

export const LoginPage = WithCommonProps(() => {
    const [account, setAccount] = useState("")
    const [password, setPassword] = useState("")
    const [errors, setErrors] = useState({})

    const handleAccountChange = (e) => {
        setAccount(e.target.value)
    }

    const handlePasswordChange = (e) => {
        setPassword(e.target.value)
    }

    const isValidForm = () => {
        const newErrors = {}
        const accountErrors = []
        const passwordErrors = []
        if (!account) accountErrors.push("Vui lòng nhập tài khoản")
        if (account.length > 20) accountErrors.push("Tài khoản tối đa 20 ký tự")
        if (!password) passwordErrors.push("Vui lòng nhập mật khẩu")
        if (password.length > 36) passwordErrors.push("Mật khẩu tối đa 36 ký tự")
        if (accountErrors.length > 0) newErrors['account'] = accountErrors
        if (passwordErrors.length > 0) newErrors['password'] = passwordErrors
        setErrors(newErrors)
        return accountErrors.length === 0 && passwordErrors.length === 0
    }

    const handleSubmit = () => {
        if (isValidForm()) {
            window.location.href = "/"
        }
    }

    return <div className="login-page">
        <div className="title">Đăng nhập</div>
        <div className="form">
            <div className="form-control">
                <label>Tài khoản:</label>
                <input type="text" onChange={(e) => handleAccountChange(e)} value={account} />
                {errors.account && errors.account.length > 0 && errors.account.map((message) => {
                    return <div className="error">{message}</div>
                })}
            </div>
            <div className="form-control">
                <label>Mật khẩu:</label>
                <input type="password" onChange={(e) => handlePasswordChange(e)} value={password} />
                {errors.password && errors.password.length > 0 && errors.password.map((message) => {
                    return <div className="error">{message}</div>
                })}
            </div>
            <div className="form-actions">
                <button className="btn-primary" onClick={() => handleSubmit()}>Submit</button>
            </div>
        </div>
    </div>
})
