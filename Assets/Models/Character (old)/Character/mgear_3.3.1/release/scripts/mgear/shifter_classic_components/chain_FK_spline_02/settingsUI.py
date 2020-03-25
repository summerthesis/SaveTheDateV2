import mgear.core.pyqt as gqt
QtGui, QtCore, QtWidgets, wrapInstance = gqt.qt_import()

class Ui_Form(object):
    def setupUi(self, Form):
        Form.setObjectName("Form")
        Form.resize(294, 211)
        self.gridLayout = QtWidgets.QGridLayout(Form)
        self.gridLayout.setObjectName("gridLayout")
        self.groupBox = QtWidgets.QGroupBox(Form)
        self.groupBox.setTitle("")
        self.groupBox.setObjectName("groupBox")
        self.gridLayout_2 = QtWidgets.QGridLayout(self.groupBox)
        self.gridLayout_2.setObjectName("gridLayout_2")
        self.verticalLayout = QtWidgets.QVBoxLayout()
        self.verticalLayout.setObjectName("verticalLayout")
        self.neutralPose_checkBox = QtWidgets.QCheckBox(self.groupBox)
        self.neutralPose_checkBox.setText("Neutral Pose")
        self.neutralPose_checkBox.setObjectName("neutralPose_checkBox")
        self.verticalLayout.addWidget(self.neutralPose_checkBox)
        self.keepLength_checkBox = QtWidgets.QCheckBox(self.groupBox)
        self.keepLength_checkBox.setText("Keep Length")
        self.keepLength_checkBox.setObjectName("keepLength_checkBox")
        self.verticalLayout.addWidget(self.keepLength_checkBox)
        self.overrideNegate_checkBox = QtWidgets.QCheckBox(self.groupBox)
        self.overrideNegate_checkBox.setText("Override Negate Axis Direction For \"R\" Side")
        self.overrideNegate_checkBox.setObjectName("overrideNegate_checkBox")
        self.verticalLayout.addWidget(self.overrideNegate_checkBox)
        self.extraTweak_checkBox = QtWidgets.QCheckBox(self.groupBox)
        self.extraTweak_checkBox.setText("Extra Tweaks")
        self.extraTweak_checkBox.setObjectName("extraTweak_checkBox")
        self.verticalLayout.addWidget(self.extraTweak_checkBox)
        self.gridLayout_2.addLayout(self.verticalLayout, 0, 0, 1, 1)
        self.gridLayout.addWidget(self.groupBox, 0, 0, 1, 1)
        self.groupBox_2 = QtWidgets.QGroupBox(Form)
        self.groupBox_2.setObjectName("groupBox_2")
        self.gridLayout_3 = QtWidgets.QGridLayout(self.groupBox_2)
        self.gridLayout_3.setObjectName("gridLayout_3")
        self.overrideJntNb_checkBox = QtWidgets.QCheckBox(self.groupBox_2)
        self.overrideJntNb_checkBox.setText("Override Joints Number")
        self.overrideJntNb_checkBox.setObjectName("overrideJntNb_checkBox")
        self.gridLayout_3.addWidget(self.overrideJntNb_checkBox, 0, 0, 1, 1)
        self.horizontalLayout = QtWidgets.QHBoxLayout()
        self.horizontalLayout.setObjectName("horizontalLayout")
        self.jntNb_label = QtWidgets.QLabel(self.groupBox_2)
        self.jntNb_label.setObjectName("jntNb_label")
        self.horizontalLayout.addWidget(self.jntNb_label)
        self.jntNb_spinBox = QtWidgets.QSpinBox(self.groupBox_2)
        self.jntNb_spinBox.setMinimum(1)
        self.jntNb_spinBox.setProperty("value", 3)
        self.jntNb_spinBox.setObjectName("jntNb_spinBox")
        self.horizontalLayout.addWidget(self.jntNb_spinBox)
        self.gridLayout_3.addLayout(self.horizontalLayout, 1, 0, 1, 1)
        self.gridLayout.addWidget(self.groupBox_2, 1, 0, 1, 1)
        spacerItem = QtWidgets.QSpacerItem(20, 40, QtWidgets.QSizePolicy.Minimum, QtWidgets.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem, 2, 0, 1, 1)

        self.retranslateUi(Form)
        QtCore.QMetaObject.connectSlotsByName(Form)

    def retranslateUi(self, Form):
        Form.setWindowTitle(gqt.fakeTranslate("Form", "Form", None, -1))
        self.groupBox_2.setTitle(gqt.fakeTranslate("Form", "Joint Options", None, -1))
        self.jntNb_label.setText(gqt.fakeTranslate("Form", "Joints Number", None, -1))

