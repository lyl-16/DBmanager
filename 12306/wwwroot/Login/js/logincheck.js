
jQuery(document).ready(function() {

    
    $('.page-container form').submit(function(){
        
     


        var username = $(this).find('.username').val();
        var password = $(this).find('.password').val();
        //var usercode = $(this).find('.code').val();
        if(username == '') {
            $(this).find('.error').fadeOut('fast', function(){
                $(this).css('top', '27px');
                //alert("用户名不正确！");
            });
            $(this).find('.error').fadeIn('fast', function(){
                $(this).parent().find('.username').focus();
            });
            return false;
        }
        var x = 0;
        if(password == '') {
            $(this).find('.error').fadeOut('fast', function(){
                $(this).css('top', '96px');
            });
            $(this).find('.error').fadeIn('fast', function(){
                $(this).parent().find('.password').focus();
            });
            return false;
        }
        var inputCode =document.getElementById("Captcha").value.toUpperCase(); //取得输入的验证码并转化为大写         
        if(inputCode.length <= 0) { //若输入的验证码长度为0   
          alert("请输入验证码！"); //则弹出请输入验证码   
          return false;
        }
        else if(inputCode != code ) { //若输入的验证码与产生的验证码不一致时   
          alert("验证码输入错误！"); //则弹出验证码输入错误   
          createCode();//刷新验证码   
          document.getElementById("Captcha").value = "";//清空文本框 
          return false;
        }
        else {
            x = 1;
        }

        /*if (x == 1) {
            $.ajax({
                url: "/Login/Index",
                type: "POST",    //提交方式
                data: { "username": username, "password": password },
                dataType: "json",

                success: function (data) {'
                    if(data.IsSuccess)
                    {}
                    else
                    {}
                    alert(data.username)
                    alert("后端连接成功");
                },
                //失败之后的操作
                error: function () {
                    console.log(username, password);
                    alert("登录失败！");
                }
            })

        }*/
        //return true;
    });

    $('.page-container form .username, .page-container form .password').keyup(function(){
        $(this).parent().find('.error').fadeOut('fast');
    });

});
