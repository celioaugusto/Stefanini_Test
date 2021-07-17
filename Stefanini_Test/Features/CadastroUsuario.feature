Funcionalidade: Cadastro de Usuário
				EU COMO usuário do sistema. 
				DESEJO realizar o cadastro de novos usuários. 
				PARA QUE seja possível armazenar e gerenciar seus dados.


Cenário: Validar elementos da tela de cadastro
	Dado Que eu estou na tela de cadastro de usuario
	Então Eu valido todos os componentes na tela.

Cenário: Validar alertas dos campos
	Dado Que eu estou na tela de cadastro de usuario
	Quando Eu clicar no botão Cadastrar sem informar nenhum registro
	Então Eu valido os alertas de cada campo

Cenário: Validar mensagens de açoes invalidas - Nome
		Dado Que eu estou na tela de cadastro de usuario
		Quando Eu informar o nome incompleto 'Augusto' sem sobrenome
		Então Eu devo receber o alerta com a mensagem 'Por favor, insira um nome completo.'

Cenário: Validar mensagens de açoes invalidas - Email
		Dado Que eu estou na tela de cadastro de usuario
		Quando Eu informar um email inválido 'augusto@.br'
		Então Eu devo receber o alerta com a mensagem 'Por favor, insira um e-mail válido.'

Cenário: Validar mensagens de açoes invalidas - Senha
		Dado Que eu estou na tela de cadastro de usuario
		Quando Eu informar uma senha com menos caracteres 'Teste@1'
		Então Eu devo receber o alerta com a mensagem 'A senha deve conter ao menos 8 caracteres.'

Cenário: Realizar cadastro do usuário
		Dado Que eu estou na tela de cadastro de usuario
		Quando Eu preencher o campo nome <Nome>
		E Eu preencher o campo email <Email>
		E Eu preencher a senha <Senha>
		Então Eu clico em cadastrar
		Exemplos: 
		| Nome         | Email             | Senha     |
		| Jordan Silva | j.silva@gmail.com | Teste@123 |  


Cenario: Excluir cadastro de usuario
	Dado Que eu estou na tela de cadastro de usuario
	Quando Eu cadastrar os usuarios
	| nome            | email             | senha     |
	| Jordan Silva    | j.silva@gmail.com | Teste@123 |
	| Aparecido Alves | a.alves@gmail.com | Teste@123 |
	| Jualino Souza   | j.souza@gmail.com | Teste@123 |
	| Maria Luz       | m.luz@gmail.com   | Teste@123 |
	| Julia Pera      | j.pera@gmail.com  | Teste@123 |
	
	Entao Eu quero excluir o usuario

	
	
