﻿'**********************************************************************************
'* Copyright (C) 2007,2014 Hitachi Solutions,Ltd.
'**********************************************************************************

#Region "Apache License"
'
'  
' 
'  
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License. 
' You may obtain a copy of the License at
'
' http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.
'
#End Region

'**********************************************************************************
'* クラス名        ：WinCustomTextBoxDgvCell
'* クラス日本語名  ：DataGridViewのWinCustomTextBoxセル（テンプレート）
'*
'* 作成者          ：生技 西野
'* 更新履歴        ：
'* 
'*  日時        更新者            内容
'*  ----------  ----------------  -------------------------------------------------
'*  20xx/xx/xx  ＸＸ ＸＸ         新規作成（テンプレート）
'**********************************************************************************

' System
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text

' System.Windows
Imports System.Windows
Imports System.Windows.Forms

Namespace Touryo.Infrastructure.CustomControl.RichClient
	''' <summary>DataGridViewのWinCustomTextBoxセル</summary>
	Public Class WinCustomTextBoxDgvCell
		Inherits DataGridViewTextBoxCell
		''' <summary>コンストラクタ</summary>
		Public Sub New()
			MyBase.New()
		End Sub

        '// <summary>DataGridViewTextBoxCell.InitializeEditingControl経由のset_Textを無視するための関数</summary>
		'private void _83B12F0F_CEA3_4f93_9233_B86EFA149BB2(
		'    int rowIndex, object initialFormattedValue,
		'    DataGridViewCellStyle dataGridViewCellStyle)
		'{
		'    // ベースへ
		'    base.InitializeEditingControl(rowIndex,
		'        initialFormattedValue, dataGridViewCellStyle);
		'}

		''' <summary>編集コントロールを初期化</summary>
		''' <param name="rowIndex">行</param>
		''' <param name="initialFormattedValue">フォーマットバリュー</param>
		''' <param name="dataGridViewCellStyle">セル スタイル</param>
		Public Overrides Sub InitializeEditingControl(rowIndex As Integer, initialFormattedValue As Object, dataGridViewCellStyle As DataGridViewCellStyle)
            '/ DataGridViewTextBoxCell.InitializeEditingControlを呼び出す。
			'this._83B12F0F_CEA3_4f93_9233_B86EFA149BB2(
			'    rowIndex, initialFormattedValue, dataGridViewCellStyle);

			' ベースへ
			MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)

			' 編集コントロールであるWinCustomTextBoxDgvHostの取得
			Dim winCustomTextBoxDgvHost As WinCustomTextBoxDgvHost = TryCast(Me.DataGridView.EditingControl, WinCustomTextBoxDgvHost)

			' 編集コントロールであるWinCustomTextBoxDgvHostが取得できた場合
			If winCustomTextBoxDgvHost IsNot Nothing Then
				' カスタム列のプロパティを反映させる
				Dim column As WinCustomTextBoxDgvCol = TryCast(Me.OwningColumn, WinCustomTextBoxDgvCol)

				' プロパティの移植
				If column IsNot Nothing Then
					' 追加したプロパティをコピー
					' チェック系は不要、編集系を設定
					winCustomTextBoxDgvHost.MaxLength = column.MaxLength

					winCustomTextBoxDgvHost.IsNumeric = column.IsNumeric
					winCustomTextBoxDgvHost.EditInitialValue = column.EditInitialValue
					'winCustomTextBoxDgvHost.EditAddFigure = column.EditAddFigure;
					winCustomTextBoxDgvHost.EditPadding = column.EditPadding
					'winCustomTextBoxDgvHost.EditDigitsAfterDP = column.EditDigitsAfterDP;
                    'winCustomTextBoxDgvHost.DisplayUnits = column.DisplayUnits;
					winCustomTextBoxDgvHost.EditDigitsAfterDP_Editing = column.EditDigitsAfterDP_Editing
				End If

				' Textを設定（３項演算）

                '/ バインド先を変更
					'if (winCustomTextBoxDgvHost.DisplayUnits == null)
					'{
					'    // Textを設定（３項演算）
					'    winCustomTextBoxDgvHost.Text =
					'        this.Value == null ? "" : this.Value.ToString();
					'}
					'else
					'{
					'    // Valueを設定
					'    winCustomTextBoxDgvHost.Value = this.Value;
                '}

                'Try

                'System.Diagnostics.Debug.WriteLine("InitializeEditingControl")
                'System.Diagnostics.Debug.WriteLine("・this.RowIndex：" & Me.RowIndex.ToString())

                ' DataGridView で DateTimePicker をホストすると ArgumentOutOfException が発生する
                'http://social.msdn.microsoft.com/Forums/ja-JP/7079fb1c-d171-44f8-81b1-751f3fe1ba6f/datagridview-datetimepicker-argumentoutofexception-

                'winCustomTextBoxDgvHost.Text = If(Me.Value Is Nothing, "", Me.Value.ToString())
                winCustomTextBoxDgvHost.Text = If(Me.GetValue(rowIndex) Is Nothing, "", Me.GetValue(rowIndex).ToString())

                'Catch aoorEx As ArgumentOutOfRangeException
                '    ' この例外は潰す。
                'End Try

            End If
		End Sub

		''' <summary>編集コントロールの型を指定する</summary>
		Public Overrides ReadOnly Property EditType() As Type
			Get
				Return GetType(WinCustomTextBoxDgvHost)
			End Get
		End Property

        '// <summary>セルの値のデータ型を指定する。</summary>
        '// <remarks>ここでは、Object型とする。基本クラスと同じなので、オーバーライドの必要なし</remarks>
		'public override Type ValueType
		'{
		'    get
		'    {
		'        return typeof(object);
		'    }
		'}

		''' <summary>新しいレコード行のセルの既定値を指定する</summary>
		Public Overrides ReadOnly Property DefaultNewRowValue() As Object
			Get
				' ベースへ。
				Return MyBase.DefaultNewRowValue
			End Get
		End Property
	End Class
End Namespace
